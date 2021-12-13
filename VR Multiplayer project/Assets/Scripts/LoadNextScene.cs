using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadNextScene : NetworkBehaviour
{
    bool nurseCheck;
    bool agressorCheck;
    float timeRemaining = 15; // normaal 70, slechts 10 om te testen

    Scene scene;

    GameObject gameManager;

    private void Start()
    {
        if (this == isServer)
        {
            gameManager = GameObject.Find("GameManager");
        }
    }

    private void Update()
    {
        scene = SceneManager.GetActiveScene();

        if (scene.name == "IntroductionRoom")
        {
            timeRemaining -= Time.deltaTime;

        }
    }


    public void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Nurse")
        {
            if (this == isServer)
            {
                Debug.Log("NurseOnSpawn");
                if (scene.name == "Wachtkamer")
                {
                    gameManager.GetComponent<GameManager>().GoToIntroductionRoom(1,1);
                }
                if (scene.name == "IntroductionRoom" && timeRemaining <= 0)
                {
                    gameManager.GetComponent<GameManager>().GoToHospitalRoom(1,1);
                }
            }
        }
        else if (other.tag == "Agressor")
        {
            if (this == isServer)
            {
                Debug.Log("AgressorOnSpawn");
                if(scene.name == "Wachtkamer")
                {
                    gameManager.GetComponent<GameManager>().GoToIntroductionRoom(2,1);
                }
                if (scene.name == "IntroductionRoom" && timeRemaining <= 0)
                {
                    gameManager.GetComponent<GameManager>().GoToHospitalRoom(2,1);
                }
            }
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.tag == "Nurse")
        {
            if (this == isServer)
            {
                if (scene.name == "Wachtkamer")
                {
                    gameManager.GetComponent<GameManager>().GoToIntroductionRoom(1,0);
                }
                if (scene.name == "IntroductionRoom" && timeRemaining <= 0)
                {
                    gameManager.GetComponent<GameManager>().GoToHospitalRoom(1,0);
                }
            }
        }
        else if (other.tag == "Agressor")
        {
            if (this == isServer)
            {
                if (scene.name == "Wachtkamer")
                {
                    gameManager.GetComponent<GameManager>().GoToIntroductionRoom(2,0);
                }
                if (scene.name == "IntroductionRoom" && timeRemaining <= 0)
                {
                    gameManager.GetComponent<GameManager>().GoToHospitalRoom(2,0);
                }
            }
        }
    }
}