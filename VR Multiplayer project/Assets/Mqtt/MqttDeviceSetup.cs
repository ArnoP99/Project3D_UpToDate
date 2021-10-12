using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class MqttDeviceSetup : MonoBehaviour {

    public static List<Device> Devices = new List<Device>();  //A list that contains all devices with MQTT traffic
    public static int MaxMQTTMessageBuffer = 10; //max buffered mqtt messages (per decvcie)
    public static List <int> ToLink = new List<int>(); //List with deviceindex of the devices to link

    public class Device // Class for a single device
    {
        public int Type { get; set; } = 1;
        public string MacAddress { get; set; } = "";
        public int MqttBrokerId { get; set; } = 1;
        public int PlayerId { get; set; } = -1;
        public bool Linked2Player { get; set; } = false;
        public bool Connected { get; set; } = false;
        public int Id { get; set; } = -1;
        public int ModelIndex { get; set; } = -1;
        public float BatteryVoltage { get; set; } = 0;
        public DeviceStates DeviceState { get; set; } = DeviceStates.Offline;
        public DeviceButtons Button { get; set; } = new DeviceButtons { };
        public List<DeviceMessage> MessageBuffer = new List<DeviceMessage>(); //list to receiveq/queued messages. There is a max number of messages defined (maxMQTTMessageBuffer)
    }

    public class DeviceMessage // Class that holds all the messages (Device --> Server for a specific device) 
    {
        public byte MsgOpcode;
        public List<byte> MsgData = new List<byte>();
      //  public List<byte> FullData = new List<byte>();
    }

    public class DeviceButtons // Different buttons for a device
    {
        public Button Trigger { get; set; } = new Button();
        public Button LeftFront { get; set; } = new Button();
        public Button RightFront { get; set; } = new Button();
        public Button LeftBack { get; set; } = new Button();
        public Button RightBack { get; set; } = new Button();
        public Button Bottom { get; set; } = new Button();
    }

    public class Button // Button class, to manage the status of a button
    {
        public bool Down { get; set; } = false;
        public bool Up { get; set; } = false;
        public bool Pressed { get; set; } = false;
    }

    public enum DeviceStates
    {
        Offline, // in gebruik
        ToLink,
        Connected, // in gebruik
        Ready4Game,
        InGame,
        HapticsOk,
        Announce, // niet in gebruik ?
        Standby,
        Game,
        RequestChangeMode
    }

    public enum BlasterButtons : byte // Byte value for every individual button
    {
        None = 0x00,
        Trigger = 0x01,
        LeftFront = 0x02,
        RightFront = 0x04,
        LeftBack = 0x08,
        RightBack = 16,
        Bottom = 32
    }

    public enum OPCODE_IN_GEN : byte
    {
        Announce = 0xAA,
        BlasterPing = 0xBC,
        RequestConnect = 0xB1,
        ReportHapticStatus = 0xBF,
        ConfirmEnterStandby = 0x34,
        ReqHapticMessage = 0x71,
        ModeChangeComplete = 0x12,
        TransferHapticMessage = 0x72,
        ConfirmEnterGame = 0x33,
        BatteryStatus = 0xF4
    }


    public enum IN_OPCODES  // all opcodes (message types, both directions not all of them are implemented in the striker)
    {
        Announce = 0xAA,
        BlasterPing = 0xBC,
        RequestConnect = 0xB1,
        ReportHapticStatus = 0xBF,
        ConfirmEnterStandby = 0x34,
        ReqHapticMessage = 0x71,
        ModeChangeComplete = 0x12,
        TransferHapticMessage = 0x72,
        ServerResponse = 0xAD,
        IdentifyRequest = 0xA4,
        ConfirmConnect = 0xB2,
        RequestHapticStatus = 0xB7,
        RequestEnterGame = 0xB8,
        ConfirmEnterGame = 0x33,
        RequestDisconnect = 0xED,
        ConfirmDisconnect = 0xEB,
        RequestEnterStandby = 0xEC,
        SetDeviceName = 0xA2,
        DriveError = 0xEE,
        InitGame = 0xA0,
        GameStarted = 0xB0,
        InitBlaster = 0xB3,
        InitTransfer = 0x70,
        ReportTransferFailure = 0x73,
        PlayHaptic = 0xB4,
        ChangeMode = 0xB5,
        StopPlayingHaptics = 0xB6,
        BlasterUserConditions = 0xB9,
        BlasterButtonEvents = 0xC0,
        BlasterTouchpadLeft = 0xC1,
        BlasterTouchpadRight = 0xC2,
        BlasterTemperature = 0xC3,
        SetColor = 0xA3,
        Authenticate = 0xEA,
        ConnectToBlasterHub = 0xE0,
        DisconnectFromBlasterHub = 0xE1,
        BlasterConnected = 0xE2,
        BlasterDisconnected = 0xE3,
        GameConnected = 0xE4,
        GameDisconnected = 0xE5,
        BindAddressToPlayer = 0xE6,
        ConfirmBinding = 0x38,
        UnbindAddressFromPlayer = 0xE7,
        StartGame = 0xE8,
        OperationStatus = 0xEF,
        BlasterTimedOut = 0xE9,
        ConfirmEnterFactory = 0xF2,
        ConfirmExitFactory = 0xF3,
        EnableHaptics = 0x92,
        DisableHaptics = 0x93,
        DaemonPing = 0xBB,
        SetActive = 0xA5,
        SetInactive = 0xA6,
        Reset = 0xA7,
        PowerOff = 0xA8,
        RequestDiagnostic = 0xA1,
        SetName = 0xA2,
        StopHaptic = 0xB6,
        CFSMUserCondition = 0xB9,
        EnterFactory = 0xF0,
        ExitFactory = 0xF1,
        EnableTp = 0x90,
        DisableTp = 0x91,
        BatteryStatus = 0xF4
    }



    public static void InitDevices(string StrikerMac)
    { // creates an element in the devices list for every device in the macaddres list
        MqttDeviceSetup.Devices.Clear();
        #if UNITY_EDITOR
        Debug.LogFormat("MQTTDeviceSetup-InitDevices: Create an entry in the devices list for every device mac address, to store messages");
        #endif
        var i = 1;

            var newdevice = new MqttDeviceSetup.Device
            {
                MacAddress = StrikerMac,      ///Regex.Replace(StrikerMac, "[^a-fA-F0-9]", ""),
                MqttBrokerId = 1, // moet 1 of 2 zijn, afhankelijk van op welke broker de data toekomt
                MessageBuffer = new List<MqttDeviceSetup.DeviceMessage>(),
                Type = 1,
                Id = i,
                Linked2Player = false,
                PlayerId = -1,
                DeviceState = DeviceStates.Offline
            };
            MqttDeviceSetup.Devices.Add(newdevice);
            i++;
    }
}
