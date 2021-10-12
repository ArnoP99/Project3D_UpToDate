using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class BlasterAssault : MonoBehaviour
{


    public BlasterController Controller;

    public int ThisWeaponModelIndex = 1;

    private bool autoreloaddoneAssault = false;
    private bool autoreloaddoneGrenade = false;
    public bool _hasSwitchStarted;
    public enum PlayHaptics0 { AUTOFIRE = 255, RELOAD = 2, JAM =2 }
    public enum PlayHaptics1 { SINGLE_SHOT = 7, RELOAD = 8, JAM = 9, MODE_SWITCH = 4 }

    long Shots = 0;
    long Reload = 0;
    long Switch = 0;
    long Shield = 0;
    float TimeGun = 0f;
    bool preshieldstate = false;
    bool startup = true;

    bool triggered = false;

    enum States
    {
        None,
        Ready,
        Empty
    }

    bool _isFiring = false;

    #region HapticEnum
    private void Start()
    {
        startup = true;
    }
    #endregion

    // Update is called once per frame
    public void DoUpdate(List<MqttDeviceSetup.DeviceButtons> _Buttons)
    {

            MqttDeviceSetup.Device Device = MqttDeviceSetup.Devices[0];

            if (Device != null)
            {


                if (Device.Connected)
                {

                if (ThisWeaponModelIndex != Device.ModelIndex || startup) // na fyische wapenwissel kan het zijn dat het wapen nog foutief staat
                {
                    startup = false;
                    Device.DeviceState = MqttDeviceSetup.DeviceStates.RequestChangeMode; //SVdH Update the device status                  
                    BlasterOperator.SwitchWeapon(ThisWeaponModelIndex, Device.MacAddress);
#if UNITY_EDITOR
                    Debug.Log("Sync weaponindex for device: " + Device.MacAddress);
#endif
                    Device.ModelIndex = ThisWeaponModelIndex;
                }


                //  Debug.Log("Connected BlasterSniper: MyBlaster.Getplayerid: " + MyBlaster.GetPlayerID().ToString() + " IsConnected: " + IsConnected.ToString());

                    foreach (MqttDeviceSetup.DeviceButtons b in _Buttons)
                    {

                        if (b.Trigger.Pressed) // (Mag trigger up & shieldPressed false zijn ??) 
                        {
                            if (ThisWeaponModelIndex == 0)
                            {
                                Debug.Log("Trigger Pressed");
                                HandleAutoShot();
                            }

                        }

                        if (b.Trigger.Up) // (Mag trigger up & shieldPressed false zijn ??) 
                        {
                            if (ThisWeaponModelIndex == 1)
                            {
                                Debug.Log("Trigger Pressed");
                                HandleSingleShot();
                            }

                        }


                        else if (b.Bottom.Up)
                        {
                            HandleReload();
                            Debug.Log("Trigger Bottom Up");
                        }
                        else if ((b.LeftBack.Up) || (b.RightBack.Up))
                        {
                            HandleJam();

                            Debug.Log("Trigger Back Up");



                        }

                        if ((b.LeftFront.Up) && (b.RightFront.Up))
                        {
                            HandleJam();
                            Debug.Log("Trigger Front Up");
                        }


                    }



                
                     
                    
                }
            }
        
    }


    private void HandleReload()
    {
        MqttDeviceSetup.Device Device = MqttDeviceSetup.Devices[0];

        if (Device != null)
        {
            if (ThisWeaponModelIndex == 0)
            {
                BlasterOperator.PlayModeSlot((int)PlayHaptics0.RELOAD, Device.MacAddress);//PlayModeSlot((int)ModeSlots.RELOAD);
                BlasterOperator.BlasterUserConditions(0xFE, Device.MacAddress);
            }
            else if (ThisWeaponModelIndex == 1)
            {
                BlasterOperator.PlayModeSlot((int)PlayHaptics1.RELOAD, Device.MacAddress);//PlayModeSlot((int)ModeSlots.RELOAD);
                BlasterOperator.BlasterUserConditions(0xFE, Device.MacAddress);
            }
        }

    }

    private void HandleAutoShot()
    {
        MqttDeviceSetup.Device Device = MqttDeviceSetup.Devices[0];

        if (Device != null)
        {

            if (ThisWeaponModelIndex == 0)
            {
                BlasterOperator.PlayModeSlot((int)PlayHaptics0.AUTOFIRE, Device.MacAddress);//PlayModeSlot((int)ModeSlots.RELOAD);
                BlasterOperator.BlasterUserConditions(0xFE, Device.MacAddress);
            }

        }

    }

    private void HandleJam()
    {
        MqttDeviceSetup.Device Device = MqttDeviceSetup.Devices[0];

        if (Device != null)
        {

            if (ThisWeaponModelIndex == 0)
            {
                BlasterOperator.PlayModeSlot((int)PlayHaptics0.JAM, Device.MacAddress);//PlayModeSlot((int)ModeSlots.RELOAD);
                BlasterOperator.BlasterUserConditions(0xFE, Device.MacAddress);
            }
            else if (ThisWeaponModelIndex == 1)
            {
                BlasterOperator.PlayModeSlot((int)PlayHaptics1.JAM, Device.MacAddress);//PlayModeSlot((int)ModeSlots.RELOAD);
                BlasterOperator.BlasterUserConditions(0xFE, Device.MacAddress);
            }
        }

    }

    private void HandleSingleShot()
    {
        MqttDeviceSetup.Device Device = MqttDeviceSetup.Devices[0];

        if (Device != null)
        {

            if (ThisWeaponModelIndex == 1)
            {
                BlasterOperator.PlayModeSlot((int)PlayHaptics1.SINGLE_SHOT, Device.MacAddress);//PlayModeSlot((int)ModeSlots.RELOAD);
                BlasterOperator.BlasterUserConditions(0xFE, Device.MacAddress);
            }

        }

    }

}