using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadNextScene : NetworkBehaviour
{
    bool nurseCheck;
    bool agressorCheck;

    Scene scene;

    float timeRemaining = 70;

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Nurse")
        {
            if (this == isServer)
            {
                scene = SceneManager.GetActiveScene();
                if (scene.name == "Wachtkamer")
                {

                    NetworkManager.singleton.ServerChangeScene("IntroductionRoom");
                }
                if (scene.name == "IntroductionRoom")
                {
                    if (timeRemaining <= 0)
                    {
                        NetworkManager.singleton.ServerChangeScene("ZiekenhuisKamer");
                    }
                }

            }


        }
        //else if (other.tag == "Agressor")
        //{
        //    //SceneManager.LoadScene("ZiekenhuisKamer");
        //    if (this == isServer)
        //    {

        //        //NetworkManager.singleton.ServerChangeScene("ZiekenhuisKamer");
        //        NetworkManager.singleton.ServerChangeScene("IntroductionRoom");
        //    }
        //}
    }

    private void Update()
    {
        if (scene.name == "IntroductionRoom")
        {
            timeRemaining -= Time.deltaTime;
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
}
