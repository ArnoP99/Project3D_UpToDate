using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadNextScene : NetworkBehaviour
{

    GameManager gamemanager;

    private void Start()
    {
    }

    public void OnTriggerStay(Collider other)
    {
        if (other.tag == "Nurse")
        {
            Debug.Log("NurseOnSpawn");
            gamemanager.PrepNextScene(1);
        }
        else if (other.tag == "Agressor")
        {
            Debug.Log("AgressorOnSpawn");
            gamemanager.PrepNextScene(2);
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.tag == "Nurse")
        {
            Debug.Log("NurseOffSpawn");
            gamemanager.PrepNextScene(1);
        }
        else if (other.tag == "Agressor")
        {
            Debug.Log("AgressorOffSpawn");
            gamemanager.PrepNextScene(2);
        }
    }
}
