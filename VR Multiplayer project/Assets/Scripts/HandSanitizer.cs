using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandSanitizer : NetworkBehaviour
{
    public GameObject nurse;
    public GameObject agressor;
    public GameObject[] tempPlayers;

    ConversationManager conversationManager;
    ParticleSystem handSanitizer;
    bool firstTime = true;
    private void OnTriggerEnter(Collider other)
    {
        if (gameObject.name == "SoapDispenserHallway")
        {
            handSanitizer = GameObject.Find("Sanitizer_ParticleSystem").GetComponent<ParticleSystem>();

            handSanitizer.Play();

            if (other.transform.parent.transform.parent.transform.GetChild(2).gameObject.tag == "Nurse" && firstTime)
            {
                firstTime = false;
                InitConvo();
            }
        }
        else if (gameObject.name == "SoapDispenserRoom")
        {
            handSanitizer = GameObject.Find("Sanitizer_ParticleSystem_1").GetComponent<ParticleSystem>();

            handSanitizer.Play();
        }



    }

    public void InitConvo()
    {
        conversationManager = GameObject.Find("ConversationManager").gameObject.GetComponent<ConversationManager>();

        tempPlayers = GameObject.FindGameObjectsWithTag("Player");

        foreach (var tempPlayer in tempPlayers)
        {
            if (tempPlayer.GetComponent<NetworkIdentity>().isLocalPlayer == true && tempPlayer.GetComponent<NetworkIdentity>().isClient == true && tempPlayer.transform.GetChild(0).transform.GetChild(2).gameObject.tag == "Nurse")
            {
                nurse = tempPlayer;
                conversationManager.StartConversation(nurse);
            }
            else if (tempPlayer.GetComponent<NetworkIdentity>().isLocalPlayer == true && tempPlayer.GetComponent<NetworkIdentity>().isClient == true && tempPlayer.transform.GetChild(0).transform.GetChild(2).gameObject.tag == "Agressor")
            {
                agressor = tempPlayer;
            }
        }
    }
}

