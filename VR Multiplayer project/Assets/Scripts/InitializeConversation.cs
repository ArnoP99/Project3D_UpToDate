using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitializeConversation : NetworkBehaviour
{
    public GameObject nurse;
    public GameObject agressor;
    public GameObject[] tempPlayers;


    private void Start()
    {

        tempPlayers = GameObject.FindGameObjectsWithTag("Player");

        foreach (var tempPlayer in tempPlayers)
        {
            if (tempPlayer.GetComponent<NetworkIdentity>().isLocalPlayer == true && tempPlayer.GetComponent<NetworkIdentity>().isClient == true && tempPlayer.transform.GetChild(0).transform.GetChild(2).gameObject.tag == "Nurse")
            {
                nurse = tempPlayer;
                ConversationManager.Instance.StartConversation(nurse);
            }
            else if (tempPlayer.GetComponent<NetworkIdentity>().isLocalPlayer == true && tempPlayer.GetComponent<NetworkIdentity>().isClient == true && tempPlayer.transform.GetChild(0).transform.GetChild(2).gameObject.tag == "Agressor")
            {
                agressor = tempPlayer;
            }
        }
    }
}
