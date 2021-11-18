using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadNextScene : NetworkBehaviour
{
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
                gameManager.GetComponent<GameManager>().PrepNextScene(1);
            }
        }
        else if (other.tag == "Agressor")
        {
            if (this == isServer)
            {
                gameManager.GetComponent<GameManager>().PrepNextScene(2);
            }
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.tag == "Nurse")
        {
            if (this == isServer)
            {
                gameManager.GetComponent<GameManager>().PrepNextScene(1);
            }
        }
        else if (other.tag == "Agressor")
        {
            if (this == isServer)
            {
                gameManager.GetComponent<GameManager>().PrepNextScene(2);
            }
        }
    }
}
