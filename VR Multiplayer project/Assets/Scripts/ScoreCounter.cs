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
    private void OnTriggerExit(Collider other)
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

    public void sendScoreToPlayer(int activePlayer)
    {
        Debug.Log("ActivePlayer: " + activePlayer);


        if (activePlayer == 0)
        {
            GameObject nurseBar = GameObject.Find("NurseBar");

            nurseBar.GetComponent<ScoreBar>().SetScore(s_nurseScore);

            player = GameObject.FindGameObjectWithTag("Nurse").transform.parent.transform.parent.gameObject;
            //Debug.Log("player: " + player.tag);
            if (player.GetComponent<NetworkIdentity>().isLocalPlayer)
            {
                player.GetComponent<AssignAuth>().ExecuteCmdSendPlayerScore(s_nurseScore, 0);
            }
        }
        else if (activePlayer == 1)
        {
            GameObject agressorBar = GameObject.Find("AgressorBar");

            agressorBar.GetComponent<ScoreBar>().SetScore(s_agressorScore);

            player = GameObject.FindGameObjectWithTag("Agressor").transform.parent.transform.parent.gameObject; 
            //Debug.Log("player: " + player.tag);
            if (player.GetComponent<NetworkIdentity>().isLocalPlayer)
            {
                player.GetComponent<AssignAuth>().ExecuteCmdSendPlayerScore(s_agressorScore, 1);
            }
        }
    }
}
