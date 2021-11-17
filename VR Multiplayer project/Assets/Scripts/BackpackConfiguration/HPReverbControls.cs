using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class HPReverbControls : MonoBehaviour
{
    GameObject nurse;
    GameObject textPopUp;
    GameObject activeChoice;
    bool triggerValue = true;

    public void PressTrigger(InputAction.CallbackContext context)
    {
        //nurse = GameObject.FindGameObjectWithTag("Nurse");
        //textPopUp = nurse.transform.GetChild(0).transform.GetChild(3).gameObject;

        //if (textPopUp.transform.GetChild(0).GetComponent<TextMeshPro>().color == Color.red)
        //{
        //    activeChoice = textPopUp.transform.GetChild(0).gameObject;
        //    textPopUp.SetActive(false);
        //}
        //else if (textPopUp.transform.GetChild(1).GetComponent<TextMeshPro>().color == Color.red)
        //{
        //    activeChoice = textPopUp.transform.GetChild(1).gameObject;
        //    textPopUp.SetActive(false);
        //}
        //else if (textPopUp.transform.GetChild(2).GetComponent<TextMeshPro>().color == Color.red)
        //{
        //    activeChoice = textPopUp.transform.GetChild(2).gameObject;
        //    textPopUp.SetActive(false);
        //}
        //else
        //{
        //    Debug.Log("No Active Choice Found.");
        //}

        Debug.Log("Trigger Pressed");
    }

    public void Joystick(InputAction.CallbackContext context)
    {
        //Debug.Log("Joystick");
    }

    public void PrimaryButton(InputAction.CallbackContext context)
    {
        Debug.Log("PrimaryButton Pressed");

        if (triggerValue == true)
        {
            this.transform.GetChild(0).transform.GetChild(1).transform.GetChild(0).GetComponent<BoxCollider>().isTrigger = false;

            this.transform.GetChild(0).transform.GetChild(1).transform.GetChild(1).GetComponent<BoxCollider>().isTrigger = false;

            triggerValue = false;
        }
        else if (triggerValue == false)
        {
            this.transform.GetChild(0).transform.GetChild(1).transform.GetChild(0).GetComponent<BoxCollider>().isTrigger = true;

            this.transform.GetChild(0).transform.GetChild(1).transform.GetChild(1).GetComponent<BoxCollider>().isTrigger = true;

            triggerValue = true;
        }
    }
}
