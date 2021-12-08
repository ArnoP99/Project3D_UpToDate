using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadNextScene : NetworkBehaviour
{
    bool nurseCheck;
    bool agressorCheck;

    GameObject gameManager;

    private void Start()
    {
        if (this == isServer)
        {
            gameManager = GameObject.Find("GameManager");
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Nurse")
        {
            if (this == isServer)
            {
                Debug.Log("NurseOnSpawn");
                gameManager.GetComponent<GameManager>().ChangeScene(1);
            }
        }
        else if (other.tag == "Agressor")
        {
            if (this == isServer)
            {
                Debug.Log("AgressorOnSpawn");
                gameManager.GetComponent<GameManager>().ChangeScene(2);
            }
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.tag == "Nurse")
        {
            if (this == isServer)
            {
                gameManager.GetComponent<GameManager>().ChangeScene(1);
            }
        }
        else if (other.tag == "Agressor")
        {
            if (this == isServer)
            {
                gameManager.GetComponent<GameManager>().ChangeScene(2);
            }
        }
    }
}