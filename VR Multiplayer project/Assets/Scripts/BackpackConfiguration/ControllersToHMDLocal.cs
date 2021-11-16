using UnityEngine;
using UnityEngine.XR;

public class ControllersToHMDLocal : MonoBehaviour
{
    void FixedUpdate()
    {
        Vector3 pos = Vector3.zero;

        if (TryGetCenterEyePosition(out pos))
        {
            //Set it's position as the HMD negative position to substract additional movement on the controllers if the HMD is on 3DOF.
            transform.localPosition = -pos;
        }
    }

    bool TryGetCenterEyePosition(out Vector3 position)
    {
        InputDevice device = InputDevices.GetDeviceAtXRNode(XRNode.CenterEye);
        if (device.isValid)
        {
            if (device.TryGetFeatureValue(CommonUsages.centerEyePosition, out position))
            {
                return true;
            }
        }
        // This is the fail case, where there was no center eye was available.
        position = Vector3.zero;
        return false;
    }

    //On Disable the script just use the normal position for the controllers.
    private void OnDisable()
    {
        transform.localPosition = Vector3.zero;
    }
}
