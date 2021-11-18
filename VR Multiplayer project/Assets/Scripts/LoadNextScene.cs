using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadNextScene : NetworkBehaviour
{

    GameManager gamemanager;
    int counter = 0;

    private void Start()
    {
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Nurse")
        {
            counter += 1;
            CmdNurseCheck();

        }
        else if (other.tag == "Agressor")
        {
            counter += 1;
            CmdAgressorCheck();
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.tag == "Nurse")
        {
            Debug.Log("NurseOffSpawn");
            GameManager.PrepNextScene(1, counter);
        }
        else if (other.tag == "Agressor")
        {
            Debug.Log("AgressorOffSpawn");
            GameManager.PrepNextScene(2,counter);
        }
    }

    [Command(requiresAuthority = false)]
    public void CmdNurseCheck()
    {
        Debug.Log("NurseOnSpawn");
        GameManager.PrepNextScene(1,counter);
    }

    [Command(requiresAuthority = false)]
    public void CmdAgressorCheck()
    {
        Debug.Log("AgressorOnSpawn");
        GameManager.PrepNextScene(2,counter);
    }

}
