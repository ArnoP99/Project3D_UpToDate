using Mirror;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreCounter : NetworkBehaviour
{
    int s_agressorScore = 0;


    int s_nurseScore = 0;

    GameObject player;

    void Start()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        if (this == isClient && this != isServer && this == isLocalPlayer)
        {
            if (other.gameObject.layer == 9 && this.gameObject.tag == "AgressorBox")
            {
                s_agressorScore += 10;
                sendScoreToPlayer(1);
            }
            else if (other.gameObject.layer == 9 && this.gameObject.tag == "NurseBox")
            {
                s_nurseScore += 10;
                sendScoreToPlayer(0);
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (this == isClient && this != isServer && this == isLocalPlayer)
        {
            if (other.gameObject.layer == 9 && this.gameObject.tag == "AgressorBox")
            {
                if (s_agressorScore > 0)
                {
                    s_agressorScore -= 10;
                    sendScoreToPlayer(1);
                }
            }
            else if (other.gameObject.layer == 9 && this.gameObject.tag == "NurseBox")
            {
                if (s_nurseScore > 0)
                {
                    s_nurseScore -= 10;
                    sendScoreToPlayer(0);
                }
            }
        }
    }

    public void sendScoreToPlayer(int activePlayer)
    {

        if (activePlayer == 0)
        {
            player = GameObject.FindGameObjectWithTag("Nurse");
            Debug.Log("player: " + player.tag);
            player.GetComponent<AssignAuth>().ExecuteCmdSendPlayerScore(s_nurseScore, 0);
        }
        else if (activePlayer == 1)
        {
            player = GameObject.FindGameObjectWithTag("Agressor");
            Debug.Log("player: " + player.tag);
            player.GetComponent<AssignAuth>().ExecuteCmdSendPlayerScore(s_agressorScore, 1);
        }
    }
}
