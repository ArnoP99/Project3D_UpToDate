using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadNextScene : NetworkBehaviour
{
    bool nurseCheck;
    bool agressorCheck;

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Nurse")
        {
            //if (this == isServer)
            //{
            //    NetworkManager.singleton.ServerChangeScene("ZiekenhuisKamer");
            //}

            RpcChangeScene();
        }
        else if (other.tag == "Agressor")
        {
            //SceneManager.LoadScene("ZiekenhuisKamer");
            //if (this == isServer)
            //{
            RpcChangeScene();
            //}
        }
    }

    //public void OnTriggerExit(Collider other)
    //{
    //    if (other.tag == "Nurse")
    //    {
    //        if (this == isServer)
    //        {
    //            gameManager.GetComponent<GameManager>().PrepNextScene(1);
    //        }
    //    }
    //    else if (other.tag == "Agressor")
    //    {
    //        if (this == isServer)
    //        {

    //            gameManager.GetComponent<GameManager>().PrepNextScene(2);
    //        }
    //    }
    //}

    [ClientRpc]
    public void RpcChangeScene()
    {
        NetworkManager.singleton.ServerChangeScene("ZiekenhuisKamer");
    }
}
