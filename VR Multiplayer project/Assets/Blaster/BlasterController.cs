using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(1000)]
public class BlasterController : MonoBehaviour
{
    bool _isConnected;

    public BlasterAssault _BlasterAssault;


    private List<MqttDeviceSetup.DeviceButtons> _Buttons = new List<MqttDeviceSetup.DeviceButtons>();




    int _strikerWeaponID = -1;
    public int StrikerWeaponID
    {
        get { return _strikerWeaponID; }
        set { _strikerWeaponID = value; }
    }


    public float FireDelayTimer;





    private float connectiontime;
    private bool connectedtrigger = false;
    private bool preconnected = false;

    private void Update()
    {









                var _Device = MqttDeviceSetup.Devices[0];
           
                if (_Device != null)
                {

                    if (_Device.Connected)
                    {


                            connectiontime += Time.deltaTime;


                        _Buttons.Clear();

                        var MessagesInBuffer = _Device.MessageBuffer.Count;
                        do
                        {// dus minstens 1x doorlopen
                            if (MessagesInBuffer > 0)
                            {
                                BlasterOperator.DecodeMessage(_Device);
                                _Device.MessageBuffer.RemoveAt(0);
                                MessagesInBuffer--; //
                            }
                            else
                            {
                                BlasterOperator.DecodeNoMessage(_Device);
                            }

                           


                            _Buttons.Add(_Device.Button);

                        } while (MessagesInBuffer > 0);


                            _BlasterAssault.DoUpdate(_Buttons);


                    }

                    if (_Device.Connected != preconnected)
                    {
                        preconnected = _Device.Connected;

                        if (_Device.Connected)
                        {
                            connectedtrigger = true;
                        }
                    }

                  
                    connectedtrigger = false;
                    connectiontime = 0;

                }
            

        

    }


}
