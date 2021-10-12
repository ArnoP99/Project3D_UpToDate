using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;
using System.IO;
using System.Linq;

public class BlasterOperator : MonoBehaviour
{
    private Dictionary<string, int> BlastersToPlayers = new Dictionary<string, int>();


    // data for haptic file transfer
    byte[] hapticDataArray;
    byte[] sizeOfHapticFile;
    ushort numHapticMessages;
    int maxBytesMessage = 2000;
    string hapticFileName = @"C:\VR\Build\HapticModeData.hptm";
    //MQTT Stuff
    public static List<MqttClient> MqttClients = new List<MqttClient>();
 //   public static MqttClient MqttClientRemote;
    public string Username = "";
    public string Password = "";
    public string[] SubscribedTopics = { "arena/2server/#" };
    public byte[] QosLevels = { 2 };
    public static string DeviceTopicRoot = "arena/2device/";
    public string MQTTBrokerIPLocal;
    public BlasterController _blastercontroller;
    public string StrikerMac;
    void Awake()  //opzetten MQTT connectie & haptic data inladen in array
    {


        Connect2MQTTBroker();

        SetHapticData();  //load Haptic data into array (once / game @ startup)

        MqttDeviceSetup.InitDevices(StrikerMac);

    }

    void Start()
    {
        UpdateLinkedStatus(StrikerMac, 1, true);
    }

    void OnDestroy() //mqtt connectie terug afsluiten
    {
        DisconnectFromBroker();

    }




    void Update() //elke run kijken of er blasters te linken zijn aan het blaster game object
    {
        var DevToLink = MqttDeviceSetup.ToLink.Count();
        if (DevToLink > 0)
        {
            for (int i = 0; i < DevToLink; i++)
            {
                BlasterOperator.SendMqtt(MqttDeviceSetup.IN_OPCODES.ServerResponse, MqttDeviceSetup.Devices[MqttDeviceSetup.ToLink.First()].MacAddress, new byte[4] { 0x00, 0x02, 0x00, 0x00 }); 
                BlasterConnect(MqttDeviceSetup.Devices[MqttDeviceSetup.ToLink.First()].MacAddress);
                MqttDeviceSetup.ToLink.RemoveAt(0);
            }
        }
    }

    public static void DisconnectAllWeapons()
    {
        foreach (MqttDeviceSetup.Device device in MqttDeviceSetup.Devices) //unlink all devices
        {
            #if UNITY_EDITOR
           // Debug.Log("Disconnect all for  " + device.MacAddress);
            #endif
            device.Linked2Player = false; //unlink
            device.PlayerId = -1; // player -1
            device.DeviceState = MqttDeviceSetup.DeviceStates.Offline;
        }
    }


    //********************************************************************************************************************************************************/
    /* Connect2MQTTBroker :gaat een connectie opzetten met de broker, en luisteren naar SubscribedTopics
     * verder wordt Client_MqttMsgPublishReceived aangeroepen bij een event van een binnenkomend bericht
     * 
     * Nog niet voorzien: foutafhandeling indien geen MQTT broker gevonden
     * Eventuele reconnect, indien connectie is weggevallen
     * Niet zeker of we beide moeten hebben, in game is het toch om zeep, zou enkel inschakelvolgorde issues opvangen
     */

    public void DisconnectFromBroker()
    {
        foreach (MqttClient t in MqttClients)
        {
            t.Disconnect();
        }
        MqttClients.Clear();
        
    }

    public void Connect2MQTTBroker(int local = 1) // to do: error handling if broker is not online
    {

            MqttClient MqttClientLocal;
            MqttClientLocal = new MqttClient(MQTTBrokerIPLocal);
            MqttClientLocal.MqttMsgPublishReceived += Client_MqttMsgPublishReceived; // register the callback
            MqttClientLocal.Connect("LocalMqttClient");
            MqttClientLocal.Subscribe(SubscribedTopics, QosLevels);
            #if UNITY_EDITOR
            Debug.LogFormat(" Connected to the Local MQTT Broker: " + MQTTBrokerIPLocal);
#endif
            MqttClients.Add(MqttClientLocal);
        
    }
    /* SetHapticData 
    * Leest de haptic file in, en plaatst deze in een byte array, om later te gebruiken. Wordt aangeroepen bij de initiele setup (awake)
    */
    private void SetHapticData() //reads the hapticdata file and store the data in a byte array. Used @ startup, byte array is thus preloaded.
    {
        hapticDataArray = ReadHapticFile(hapticFileName);//read the haptic file in a byte array
        int fileSize = (ushort)hapticDataArray.Length;
        sizeOfHapticFile = BitConverter.GetBytes((ushort)fileSize);// calculate the number of messages needed - number of bytes in the file
        if (BitConverter.IsLittleEndian)
        {
            Array.Reverse(sizeOfHapticFile);
        }
        numHapticMessages = (ushort)Math.Ceiling(
                                  (decimal)fileSize
                                / (decimal)maxBytesMessage);
    }

    /* ReadHapticFile 
     * Leest de haptic file in.
     */
    private byte[] ReadHapticFile(string filename)
    {
        using (FileStream fs = new FileStream(filename, FileMode.Open, FileAccess.Read))
        {
            byte[] bytes = System.IO.File.ReadAllBytes(filename);
            return bytes;
        }
    }


    //**************************************************************************************************************************************************************************
    // Wordt aangeroepen telkens als een mqtt message binnenkomt (asynchroon)
    // gaat kijken of het van een gebruikt wapen is, zo ja, dan afhandelen, zo nee dan er niets mee doen
    // gaat er voor zorgen dat generieke messages direct worden afgehandeld
    // outcome: 
    // - Generieke message voor actief wapen --> direct afhandelen
    // - specifieke message voor actief wapen --> opgeslagen in messagebuffer
    // - al de rest negeren we
          
    private void Client_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
    { 
        string deviceMac = e.Topic.Substring(14, 16); // mac address
        var deviceIndex = MqttDeviceSetup.Devices.FindIndex(Device => Device.MacAddress == deviceMac); //deviceindes ophalen
        if ((deviceIndex != -1) && (MqttDeviceSetup.Devices[deviceIndex].Linked2Player)) //het wapen bestaat, AND het wapen is gelinked --> Verwerken van de message
        { //device is gevonden, en linked to player, dus iets doen met deze data
            //opcode en payload uit de message halen
            var payload = new MqttDeviceSetup.DeviceMessage();
            payload.MsgOpcode = e.Message[14]; 
            payload.MsgData = e.Message.ToList();
            payload.MsgData.RemoveRange(0, 15);
            payload.MsgData.RemoveAt(payload.MsgData.Count - 1);

     //       Debug.LogError(MqttDeviceSetup.Devices.FindAll(x => x.Linked2Player == true));

            if (Enum.IsDefined(typeof(MqttDeviceSetup.OPCODE_IN_GEN), payload.MsgOpcode)) // behandel generieke opcode
            {
                HandleGenericOpcode(deviceMac, payload, deviceIndex);
            }
            else
            {
                MqttDeviceSetup.Devices[deviceIndex].MessageBuffer.Add(payload);  //Payload toevoegen aan list voor data voor dit device
                if (MqttDeviceSetup.Devices[deviceIndex].MessageBuffer.Count() > MqttDeviceSetup.MaxMQTTMessageBuffer) // check for max number of messages in the buffer - Truncate buffer
                {
                    MqttDeviceSetup.Devices[deviceIndex].MessageBuffer.RemoveAt(0); // Remove the oldest message
                }
            }
        }
    }


    //method to reply with the correct opcode message to a weapon
    private void HandleGenericOpcode(string DeviceMac, MqttDeviceSetup.DeviceMessage Payload, int deviceIndex)
    {
        switch ((MqttDeviceSetup.OPCODE_IN_GEN)Payload.MsgOpcode)
        {

            case MqttDeviceSetup.OPCODE_IN_GEN.BatteryStatus:
                 MqttDeviceSetup.Devices[deviceIndex].BatteryVoltage = (float)(ushort)((Payload.MsgData[3] << 8) + Payload.MsgData[4]) / 100; // spanning updaten voor het wapen
                 break;
            case MqttDeviceSetup.OPCODE_IN_GEN.BlasterPing: //D>S
                 HandleMqttReply(MqttDeviceSetup.IN_OPCODES.BlasterPing, DeviceMac, Payload.MsgData, deviceIndex); // antwoorden op blasterping
                 break;
            case MqttDeviceSetup.OPCODE_IN_GEN.Announce: //D>S
                 MqttDeviceSetup.Devices[deviceIndex].DeviceState = MqttDeviceSetup.DeviceStates.Announce; // status aanpassen naar announce
                 HandleMqttReply(MqttDeviceSetup.IN_OPCODES.Announce, DeviceMac, Payload.MsgData, deviceIndex); // wapen is gereset en verbonden via optitrack aan speler --> antwoorden met serverresponse
                break;
            case MqttDeviceSetup.OPCODE_IN_GEN.RequestConnect: //D>S
                HandleMqttReply(MqttDeviceSetup.IN_OPCODES.RequestConnect, DeviceMac, Payload.MsgData, deviceIndex); // antwoorden met confirmconnect opcode
                break;
            case MqttDeviceSetup.OPCODE_IN_GEN.ConfirmEnterStandby: //D>S
                //MqttDeviceSetup.ToLink.Add(deviceIndex); // zodat we sebiet in de update weten dat we op zoek moeten gaan achter een te linken device
                MqttDeviceSetup.Devices[deviceIndex].DeviceState = MqttDeviceSetup.DeviceStates.Connected; //SVdH het device dat we moeten linken
                #if UNITY_EDITOR
                Debug.Log("(BlasterOperator:HandleGenericOpcode) STATUSCHANGE: DeviceId: " + deviceIndex.ToString() + " State: " + MqttDeviceSetup.Devices[deviceIndex].DeviceState.ToString());
                #endif
                HandleMqttReply(MqttDeviceSetup.IN_OPCODES.ConfirmEnterStandby, DeviceMac, Payload.MsgData, deviceIndex); // starten met haptics status op te vragen
                break;
            case MqttDeviceSetup.OPCODE_IN_GEN.ModeChangeComplete: //D>S
                //  Debug.Log("ModeChangeComplete");
                MqttDeviceSetup.Devices[deviceIndex].DeviceState = MqttDeviceSetup.DeviceStates.Game; //SVdH Update the device status
                #if UNITY_EDITOR
                Debug.Log("(BlasterOperator:HandleGenericOpcode) STATUSCHANGE: DeviceId: " + deviceIndex.ToString() + " State: " + MqttDeviceSetup.Devices[deviceIndex].DeviceState.ToString());
                #endif
                break;
            case MqttDeviceSetup.OPCODE_IN_GEN.ReqHapticMessage: // device vraagt een haptic message
                HandleMqttReply(MqttDeviceSetup.IN_OPCODES.ReqHapticMessage, DeviceMac, Payload.MsgData, deviceIndex);
                break;
            case MqttDeviceSetup.OPCODE_IN_GEN.ReportHapticStatus: //D>S
                HandleMqttReply(MqttDeviceSetup.IN_OPCODES.ReportHapticStatus, DeviceMac, Payload.MsgData, deviceIndex);
                break;
            case MqttDeviceSetup.OPCODE_IN_GEN.ConfirmEnterGame: //D>S 
                                                                 //Device is connected, update connected status
                MqttDeviceSetup.Devices[MqttDeviceSetup.Devices.FindIndex(Device => Device.MacAddress == DeviceMac)].Connected = true; // niet zeker of we hier iets mee doen
                MqttDeviceSetup.Devices[deviceIndex].DeviceState = MqttDeviceSetup.DeviceStates.Game; //SVdH Update the device status
                #if UNITY_EDITOR
                Debug.Log("(BlasterOperator:HandleGenericOpcode) STATUSCHANGE: DeviceId: " + deviceIndex.ToString() + " State: " + MqttDeviceSetup.Devices[deviceIndex].DeviceState.ToString());
                #endif
                HandleMqttReply(MqttDeviceSetup.IN_OPCODES.ConfirmEnterGame, DeviceMac, Payload.MsgData, deviceIndex);
                break;
        }
    }



    //--------------------------------------------------------------------------------------------------------------------------------------------------------------------
    private void HandleMqttReply(MqttDeviceSetup.IN_OPCODES command, string deviceMac, List<byte> message, int deviceIndex)
    {
        switch (command)
        {
            case MqttDeviceSetup.IN_OPCODES.ReqHapticMessage: //IS OK
                int messageIndex = message[4]; // het wapen vraagt achter een specifieke message
                List<byte> xpayload = new List<byte>();
                xpayload.AddRange(new byte[5] { 0x00, 0x4F, 0x00, 0x00, 0x00 });
                xpayload.Add(message[4]);
                // uit hapticDataArray de juiste array halen. ==> hapticDataArray.Skip(messageIndex * maxBytesMessage).Take(maxBytesMessage).ToArray();
                xpayload.AddRange(hapticDataArray.Skip(messageIndex * maxBytesMessage).Take(maxBytesMessage).ToArray());
                BlasterOperator.SendMqtt(MqttDeviceSetup.IN_OPCODES.TransferHapticMessage, deviceMac, xpayload.ToArray());
                break;
            case MqttDeviceSetup.IN_OPCODES.ReportHapticStatus:
                if (message[4] == 0x00)
                {
                    BlasterOperator.SendMqtt(MqttDeviceSetup.IN_OPCODES.InitTransfer, deviceMac, new byte[11] { 0x00, 0x09, 0x00, 0x00, 0x00, 0x01, 0x00, 0x00, (byte)numHapticMessages, sizeOfHapticFile[0], sizeOfHapticFile[1] });
                }
                else
                { // initialized --> enter game mode
                    MqttDeviceSetup.Devices[deviceIndex].DeviceState = MqttDeviceSetup.DeviceStates.HapticsOk; //SVdH Update the device status
                    #if UNITY_EDITOR
                    Debug.Log("(BlasterOperator:HandleGenericOpcode) STATUSCHANGE: DeviceId: " + deviceIndex.ToString() + " State: " + MqttDeviceSetup.Devices[deviceIndex].DeviceState.ToString());
                    #endif
                    BlasterOperator.SendMqtt(MqttDeviceSetup.IN_OPCODES.RequestEnterGame, deviceMac, new byte[4] { 0x00, 0x02, 0x00, 0x00 });
                }
                break;

            case MqttDeviceSetup.IN_OPCODES.Announce: //send an answer for the Announce message
                BlasterOperator.SendMqtt(MqttDeviceSetup.IN_OPCODES.ServerResponse, deviceMac, new byte[4] { 0x00, 0x02, 0x00, 0x00 }); ;
                break;

            case MqttDeviceSetup.IN_OPCODES.RequestConnect:  //Confirm>Connect
                BlasterOperator.SendMqtt(MqttDeviceSetup.IN_OPCODES.ConfirmConnect, deviceMac, new byte[4] { 0x00, 0x02, 0x00, 0x00 });
                break;

            case MqttDeviceSetup.IN_OPCODES.BlasterPing: //deamonping
                BlasterOperator.SendMqtt(MqttDeviceSetup.IN_OPCODES.DaemonPing, deviceMac, new byte[4] { 0x00, 0x02, 0x00, 0x00 });
                break;

            case MqttDeviceSetup.IN_OPCODES.ConfirmEnterGame: //antwoorden met request mode 31
                BlasterOperator.SendMqtt(MqttDeviceSetup.IN_OPCODES.ChangeMode, deviceMac, new byte[6] { 0x00, 0x04, 0x00, 0x00, 0x00, 0x1F });
                MqttDeviceSetup.Devices[deviceIndex].DeviceState = MqttDeviceSetup.DeviceStates.RequestChangeMode; //SVdH Update the device status
                MqttDeviceSetup.Devices[deviceIndex].ModelIndex = 0; // default model index -> Assault
                #if UNITY_EDITOR
                Debug.Log("(BlasterOperator:HandleGenericOpcode) STATUSCHANGE: DeviceId: " + deviceIndex.ToString() + " State: " + MqttDeviceSetup.Devices[deviceIndex].DeviceState.ToString());
                #endif




                break;

            case MqttDeviceSetup.IN_OPCODES.ConfirmEnterStandby:
                // SVdHBlasterConnect(deviceMac);
                BlasterOperator.SendMqtt(MqttDeviceSetup.IN_OPCODES.RequestHapticStatus, deviceMac, new byte[4] { 0x00, 0x02, 0x00, 0x00 });
                break;
        }
    }


    private void BlasterConnect(string address)
    {
        //Debug.LogFormat("BlasterConnected NEW addr:{0} name:{1} port:{2}", value.Address.ToString(), value.Name, value.BlasterHub);

        var weaponID = WeaponIdFromAddress(address); //ipv de eerst volgende blaster te nemen (originele code) steken we de info van deze blaster in de blaster die overeenkomt met het weaponID           
        #if UNITY_EDITOR
        Debug.Log("BlasterOperator.cs BlasterConnect for weaponid: " + weaponID.ToString());
        #endif
        var blaster = BlasterFromStrikerPlayer();

        if (blaster != null)
        {
            blaster.StrikerWeaponID = weaponID;
            blaster.name = "Connected WeaponID: " + weaponID.ToString() + " MacAddress: " + address;
            #if UNITY_EDITOR
            Debug.Log("(BlasterOperator:SVdHBlasterConnect) Blaster IsConnected: ");
            #endif
        }
    }

    BlasterController BlasterFromStrikerPlayer()
    {

            return _blastercontroller;
        
        Debug.LogErrorFormat("BlasterFromStrikerPlayer: Error invalid striker player");
        return null;
    }


    public static void DisconnectPlayerWeapon(int player)
    {
        var deviceIndex = MqttDeviceSetup.Devices.FindIndex(Device => Device.PlayerId == player);
        if (deviceIndex != -1) // gevonden, playerlinking verwijderen
        {
#if UNITY_EDITOR
            Debug.Log("Disconnect weapon for playerid"+ player +" Device with index: "+ deviceIndex + " and macaddress: " + MqttDeviceSetup.Devices[deviceIndex].MacAddress );
#endif
            MqttDeviceSetup.Devices[deviceIndex].Linked2Player = false; // niet meer gelinked
            MqttDeviceSetup.Devices[deviceIndex].PlayerId = -1; // player -1
            MqttDeviceSetup.Devices[deviceIndex].DeviceState = MqttDeviceSetup.DeviceStates.Offline;
        }
    }

    //Wordt aangeroepen vanuit player, wanneer er een wapen door optitrack is gelinked
    public static void UpdateLinkedStatus(string macaddress, int player, bool linked)
    {
        var deviceIndex = MqttDeviceSetup.Devices.FindIndex(Device => Device.MacAddress == macaddress); //Deviceindex ophalen, aan de hand van het macadres (oldmacadres nog)
        if (deviceIndex != -1) // gevonden, linked updaten
        {
            if (linked)
            {
                var oldDeviceIndex = MqttDeviceSetup.Devices.FindIndex(Device => Device.PlayerId == player); // dit geeft het wapen waar deze speler voorheen aan hing
                if ((oldDeviceIndex != -1) && (deviceIndex != oldDeviceIndex)) // player hangt nog aan een ander device, dus de linking daar updaten naar -1
                { // opkuisen van het oude wapen aan deze player
                    MqttDeviceSetup.Devices[oldDeviceIndex].Linked2Player = false; // niet meer gelinked
                    MqttDeviceSetup.Devices[oldDeviceIndex].PlayerId = -1; // player -1
                    MqttDeviceSetup.Devices[oldDeviceIndex].DeviceState = MqttDeviceSetup.DeviceStates.Offline; // status teruggezet naar Ingame, in de veronderstelling dat het wapen nog actief staat
                                                                                                               //***** Mogelijkerwijs nog een bericht naar het wapen sturen, om het uit het spel te halen (reset ?)
                                                                                                               //of status aanpassen om de ping reply te stoppen, en zo het wapen te rebooten
                }
                // onderstaande wordt doorlopen indien
                // Het wapen eerst aan een andere speler hing, maar dat de linking daar nu verbroken isn (zie hierboven)
                // OF het wapen nog niet aan een andere speler hing
                // OF het wapen reeds aan deze speler hing
                // stuur maar al een serverresponce zonder te wachten op een announce op het proces te versnellen.
                //
                MqttDeviceSetup.ToLink.Add(deviceIndex);
                MqttDeviceSetup.Devices[deviceIndex].Linked2Player = linked; // update linked status
                MqttDeviceSetup.Devices[deviceIndex].PlayerId = player; // updaten van de player
                                                                        //Status van het nieuwe wapen hangt af van de huidige status hier mogelijkerwijs updaten
                                                                        // 
                                                                        //  MqttDeviceSetup.Devices[oldDeviceIndex].DeviceState = MqttDeviceSetup.DeviceStates.ToLink;

                // alle messages uit de buffer zwieren van dit wapen
                MqttDeviceSetup.Devices[deviceIndex].MessageBuffer.Clear();

                // einde van het verhaal: we gaan nu reageren op de generieke opcodes, het wapen dat niet meer gelinked is wordt nu genegeerd, en gaat reboten (wegens geen ping meer)


                #if UNITY_EDITOR
                Debug.LogFormat("BlasterOperator.cs--UpdateLinkedStatus: Link2Player status updated for device mac: " + macaddress);
                #endif
            }
            else
            { // linked is false --> unlink the device
                MqttDeviceSetup.Devices[deviceIndex].Linked2Player = false; // niet meer gelinked
                MqttDeviceSetup.Devices[deviceIndex].PlayerId = -1; // player -1
                MqttDeviceSetup.Devices[deviceIndex].DeviceState = MqttDeviceSetup.DeviceStates.Offline; // status teruggezet naar Ingame, in de veronderstelling dat het wapen nog actief staat
            }
        } 
        else
        {
            #if UNITY_EDITOR
            Debug.LogFormat("BlasterOperator.cs--UpdateLinkedStatus: Weapon Unknown.. Link2Player device with mac: " + macaddress + " is not found for this controller ?");
            #endif
        }
    }


    public int WeaponIdFromAddress(string address)
    {
        var deviceIndex = (MqttDeviceSetup.Devices.FindIndex(Device => Device.MacAddress == address));
        if (deviceIndex != -1)
        {
            deviceIndex += 1; //+1 to start with 1, but onlyif found (valid values = -1, 1,2,3,3...)
        }
        #if UNITY_EDITOR
        Debug.Log("BlasterOperator.cs WeaponIDFromAddress: " + address + " --> " + deviceIndex);
        #endif
        return deviceIndex;
    }

    //------------------------------------------------------------------------------------------
    public static void SendMqtt(MqttDeviceSetup.IN_OPCODES command, string deviceMac, byte[] message)
    {
        byte checksum = 0;
        int nnbytes = 14;
        List<byte> ppayload = new List<byte>();
        for (int i = 0; i < nnbytes; i++)
        {
            ppayload.Add(0x00);
        }
        ppayload[0] = 0x7E;
        ppayload[3] = 0x80;

        ppayload.Add((byte)command);
        ppayload.AddRange(message);
        nnbytes += 1 + message.Length;

        ppayload[2] = BitConverter.GetBytes((ushort)ppayload.Count - 3)[0]; // calculate number of bytes
        ppayload[1] = BitConverter.GetBytes((ushort)ppayload.Count - 3)[1];
        for (int i = 3; i < nnbytes; ++i) // calculmate checksum
            checksum += ppayload[i];
        checksum = (byte)(0xFF - checksum); //filter out the correct byte
        ppayload.Add(checksum); // add the checksum byte

        if (MqttDeviceSetup.Devices[MqttDeviceSetup.Devices.FindIndex(Device => Device.MacAddress == deviceMac)].MqttBrokerId == 1)
        {
            MqttClients.Find(x => x.ClientId == "LocalMqttClient").Publish(DeviceTopicRoot + deviceMac, ppayload.ToArray(), MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE, false);
        }
        else if (MqttDeviceSetup.Devices[MqttDeviceSetup.Devices.FindIndex(Device => Device.MacAddress == deviceMac)].MqttBrokerId == 2)
        {
            MqttClients.Find(x => x.ClientId == "RemoteMqttClient").Publish(DeviceTopicRoot + deviceMac, ppayload.ToArray(), MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE, false);
        }
    }



    public static void PlayModeSlot(int slotIndex, string macaddress)
    {
        #if UNITY_EDITOR
        Debug.Log("mqtt out : PlayModeSlot id " + slotIndex.ToString() + " for: " + macaddress);
        #endif
        switch (slotIndex)
        {

            case 2: //Reload for mode 31: 00-07-00-00-00-02-01-00-00
                BlasterOperator.SendMqtt(MqttDeviceSetup.IN_OPCODES.PlayHaptic, macaddress, new byte[9] { 0x00, 0x07, 0x00, 0x00, 0x00, 0x02, 0x01, 0x00, 0x00 });
                break;
            case 255: // Autoshoot for mode 31: 00-07-00-00-00-FF-00-00-00
                BlasterOperator.SendMqtt(MqttDeviceSetup.IN_OPCODES.PlayHaptic, macaddress, new byte[9] { 0x00, 0x07, 0x00, 0x00, 0x00, 0xFF, 0x00, 0x00, 0x00 });
                break;
            case 3:  // geen ideee
                BlasterOperator.SendMqtt(MqttDeviceSetup.IN_OPCODES.PlayHaptic, macaddress, new byte[9] { 0x00, 0x07, 0x00, 0x00, 0x00, 0xFF, 0x03, 0x00, 0x00 });
                break;
            case 7: // Single shot for mode 32: 00-07-00-00-00-07-00-00-00
                BlasterOperator.SendMqtt(MqttDeviceSetup.IN_OPCODES.PlayHaptic, macaddress, new byte[9] { 0x00, 0x07, 0x00, 0x00, 0x00, 0x07, 0x00, 0x00, 0x00 });
                break;
            case 8: //Reload for mode 32: 00-07-00-00-00-08-01-00-00 
                BlasterOperator.SendMqtt(MqttDeviceSetup.IN_OPCODES.PlayHaptic, macaddress, new byte[9] { 0x00, 0x07, 0x00, 0x00, 0x00, 0x08, 0x01, 0x00, 0x00 });
                break;
            case 9: // empty for mode 32: 00-07-00-00-00-09-02-00-00
                BlasterOperator.SendMqtt(MqttDeviceSetup.IN_OPCODES.PlayHaptic, macaddress, new byte[9] { 0x00, 0x07, 0x00, 0x00, 0x00, 0x09, 0x02, 0x00, 0x00 });
                break;
        }
    }



    public static void BlasterUserConditions(byte slotIndex, string macaddress)
    {
       BlasterOperator.SendMqtt(MqttDeviceSetup.IN_OPCODES.BlasterUserConditions, macaddress, new byte[5] { 0x00, 0x03, 0x00, 0x00, slotIndex});
    }



    public static void SwitchWeapon(int WeaponModeIndex, string macaddress) // om van wapen te wisselen
    {
        switch (WeaponModeIndex)
        {

            case 0: //Tobe = Assault:mode 31    00-04-00-00-00-1F 
                #if UNITY_EDITOR
                Debug.Log("Change Striker to Assault mode");
                #endif
                BlasterOperator.SendMqtt(MqttDeviceSetup.IN_OPCODES.StopPlayingHaptics, macaddress, new byte[0] { });
                BlasterOperator.SendMqtt(MqttDeviceSetup.IN_OPCODES.BlasterUserConditions, macaddress, new byte[5] { 0x00, 0x03, 0x00, 0x00, 0xFE });
                BlasterOperator.SendMqtt(MqttDeviceSetup.IN_OPCODES.ChangeMode, macaddress, new byte[6] { 0x00, 0x04, 0x00, 0x00, 0x00, 0x1F });

                break;
            case 1: //Tobe = WeaponSniper :mode 32 00-04-00-00-00-20 
                #if UNITY_EDITOR
                Debug.Log("Change Striker to Sniper mode");
                #endif
                BlasterOperator.SendMqtt(MqttDeviceSetup.IN_OPCODES.StopPlayingHaptics, macaddress, new byte[0] { });
                BlasterOperator.SendMqtt(MqttDeviceSetup.IN_OPCODES.ChangeMode, macaddress, new byte[6] { 0x00, 0x04, 0x00, 0x00, 0x00, 0x20 });
                break;
            case 2: //Tobe = WeaponShotgun:mode 32 00-04-00-00-00-20
                #if UNITY_EDITOR
                Debug.Log("Change Striker to Shotgun mode");
                #endif
                BlasterOperator.SendMqtt(MqttDeviceSetup.IN_OPCODES.StopPlayingHaptics, macaddress, new byte[0] { });
                BlasterOperator.SendMqtt(MqttDeviceSetup.IN_OPCODES.ChangeMode, macaddress, new byte[6] { 0x00, 0x04, 0x00, 0x00, 0x00, 0x20 });
                break;
     
            case 3: //Tobe = WeaponSniper :mode 32 00-04-00-00-00-20 
                #if UNITY_EDITOR
                Debug.Log("Change Striker to GrenadeLauncher mode");
                #endif
                BlasterOperator.SendMqtt(MqttDeviceSetup.IN_OPCODES.StopPlayingHaptics, macaddress, new byte[0] { });
                BlasterOperator.SendMqtt(MqttDeviceSetup.IN_OPCODES.ChangeMode, macaddress, new byte[6] { 0x00, 0x04, 0x00, 0x00, 0x00, 0x20 });
                break;
        }

    }

    private static void CalculateTrigger(MqttDeviceSetup.Device Device, MqttDeviceSetup.BlasterButtons xcode)
    {
        if ((xcode & MqttDeviceSetup.BlasterButtons.Trigger) == (MqttDeviceSetup.BlasterButtons.Trigger))
        {
            if (Device.Button.Trigger.Pressed) // voordien ook al ingedrukt, dus down = false
            { Device.Button.Trigger.Down = false; }
            else
            { Device.Button.Trigger.Down = true; Device.Button.Trigger.Pressed = true; }

        }
        else
        {
            if (!Device.Button.Trigger.Pressed)// was de vorige status ook niet ingedrukt ?
            { Device.Button.Trigger.Up = false; }
            else { Device.Button.Trigger.Up = true; Device.Button.Trigger.Pressed = false; }
        }
    }

    private static void CalculateBottom(MqttDeviceSetup.Device Device, MqttDeviceSetup.BlasterButtons xcode)
    {
        if ((xcode & MqttDeviceSetup.BlasterButtons.Bottom) == (MqttDeviceSetup.BlasterButtons.Bottom))
        {
            if (Device.Button.Bottom.Pressed) // voordien ook al ingedrukt, dus down = false
            { Device.Button.Bottom.Down = false; }
            else { Device.Button.Bottom.Down = true; Device.Button.Bottom.Pressed = true; }
        }
        else
        {
            if (!Device.Button.Bottom.Pressed)// was de vorige status ook niet ingedrukt ?
            { Device.Button.Bottom.Up = false; }
            else { Device.Button.Bottom.Up = true; Device.Button.Bottom.Pressed = false; }
        }
    }

    private static void CalculateLeftBack(MqttDeviceSetup.Device Device, MqttDeviceSetup.BlasterButtons xcode)
    {
        if ((xcode & MqttDeviceSetup.BlasterButtons.LeftBack) == (MqttDeviceSetup.BlasterButtons.LeftBack))
        {
            if (Device.Button.LeftBack.Pressed) // voordien ook al ingedrukt, dus down = false
            { Device.Button.LeftBack.Down = false; }
            else { Device.Button.LeftBack.Down = true; Device.Button.LeftBack.Pressed = true; }
        }
        else
        {
            if (!Device.Button.LeftBack.Pressed)// was de vorige status ook niet ingedrukt ?
            { Device.Button.LeftBack.Up = false; }
            else { Device.Button.LeftBack.Up = true; Device.Button.LeftBack.Pressed = false; }
        }
    }


    private static void CalculateRightBack(MqttDeviceSetup.Device Device, MqttDeviceSetup.BlasterButtons xcode)
    {
        if ((xcode & MqttDeviceSetup.BlasterButtons.RightBack) == (MqttDeviceSetup.BlasterButtons.RightBack))
        {
            if (Device.Button.RightBack.Pressed) // voordien ook al ingedrukt, dus down = false
            { Device.Button.RightBack.Down = false; }
            else { Device.Button.RightBack.Down = true; Device.Button.RightBack.Pressed = true; }
        }
        else
        {
            if (!Device.Button.RightBack.Pressed)// was de vorige status ook niet ingedrukt ?
            { Device.Button.RightBack.Up = false; }
            else { Device.Button.RightBack.Up = true; Device.Button.RightBack.Pressed = false; }
        }
    }

    private static void CalculateLeftFront(MqttDeviceSetup.Device Device, MqttDeviceSetup.BlasterButtons xcode)
    {
        if ((xcode & MqttDeviceSetup.BlasterButtons.LeftFront) == (MqttDeviceSetup.BlasterButtons.LeftFront))
        {
            if (Device.Button.LeftFront.Pressed) // voordien ook al ingedrukt, dus down = false
            { Device.Button.LeftFront.Down = false; }
            else { Device.Button.LeftFront.Down = true; Device.Button.LeftFront.Pressed = true; }
        }
        else
        {
            if (!Device.Button.LeftFront.Pressed)// was de vorige status ook niet ingedrukt ?
            { Device.Button.LeftFront.Up = false; }
            else { Device.Button.LeftFront.Up = true; Device.Button.LeftFront.Pressed = false; }
        }
    }


    private static void CalculateRightFront(MqttDeviceSetup.Device Device, MqttDeviceSetup.BlasterButtons xcode)
    {
        if ((xcode & MqttDeviceSetup.BlasterButtons.RightFront) == (MqttDeviceSetup.BlasterButtons.RightFront))
        {
            if (Device.Button.RightFront.Pressed) // voordien ook al ingedrukt, dus down = false
            { Device.Button.RightFront.Down = false; }
            else { Device.Button.RightFront.Down = true; Device.Button.RightFront.Pressed = true; }
        }
        else
        {
            if (!Device.Button.RightFront.Pressed)// was de vorige status ook niet ingedrukt ?
            { Device.Button.RightFront.Up = false; }
            else { Device.Button.RightFront.Up = true; Device.Button.RightFront.Pressed = false; }
        }
    }

    public static void DecodeMessage(MqttDeviceSetup.Device Device)
    {

        switch (Device.MessageBuffer.First().MsgOpcode)
        {
            case (byte)MqttDeviceSetup.IN_OPCODES.BlasterButtonEvents:
                // de huidige message vergelijken met de vorige status, om zo de nieuwe status te berekenen
                MqttDeviceSetup.BlasterButtons xcode = (MqttDeviceSetup.BlasterButtons)Device.MessageBuffer.First().MsgData[4];
                CalculateBottom(Device, xcode);
                CalculateTrigger(Device, xcode);
                CalculateLeftFront(Device, xcode);
                CalculateRightFront(Device, xcode);
                CalculateLeftBack(Device, xcode);
                CalculateRightBack(Device, xcode);
                break;
        }
    }

    public static void DecodeNoMessage(MqttDeviceSetup.Device Device)
    { //No message = reset button transitions for this device
        Device.Button.Trigger.Up = false;
        Device.Button.Trigger.Down = false;
        Device.Button.Bottom.Up = false;
        Device.Button.Bottom.Down = false;
        Device.Button.RightFront.Up = false;
        Device.Button.RightFront.Down = false;
        Device.Button.LeftFront.Up = false;
        Device.Button.LeftFront.Down = false;
        Device.Button.RightBack.Up = false;
        Device.Button.RightBack.Down = false;
        Device.Button.LeftBack.Up = false;
        Device.Button.LeftBack.Down = false;


    }




































}
