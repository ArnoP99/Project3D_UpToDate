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

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Nurse")
        {
            CmdNurseCheck();

        }
        else if (other.tag == "Agressor")
        {
            CmdAgressorCheck();
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.tag == "Nurse")
        {
            Debug.Log("NurseOffSpawn");
            GameManager.PrepNextScene(1);
        }
        else if (other.tag == "Agressor")
        {
            Debug.Log("AgressorOffSpawn");
            GameManager.PrepNextScene(2);
        }
    }

    [Command(requiresAuthority = false)]
    public void CmdNurseCheck()
    {
        Debug.Log("NurseOnSpawn");
        GameManager.PrepNextScene(1);
    }

    [Command(requiresAuthority = false)]
    public void CmdAgressorCheck()
    {
        Debug.Log("AgressorOnSpawn");
        GameManager.PrepNextScene(2);
    }

}
