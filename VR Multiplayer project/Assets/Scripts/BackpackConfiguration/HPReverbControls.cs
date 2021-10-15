using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class HPReverbControls : MonoBehaviour
{
    GameObject textPopUp;

    public void Start()
    {

        textPopUp = GameObject.FindGameObjectWithTag("ChoicePopUp");
        textPopUp.SetActive(false);
    }
    public void PressTrigger(InputAction.CallbackContext context)
    {
        Debug.Log("Trigger");
        if (textPopUp.activeInHierarchy == false)
        {
            textPopUp.SetActive(true);
        }
        else
        {
            textPopUp.SetActive(false);
        }
    }

    public void Joystick(InputAction.CallbackContext context)
    {
        Debug.Log("Joystick");
    }
}
