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
        Debug.Log(other.gameObject);
        if (other.gameObject.layer == 9 && this.gameObject.tag == "AgressorBox")
        {
            if (other.gameObject.tag == "Low")
            {
                s_agressorScore += 10;
                sendScoreToPlayer(1);
            }
            if (other.gameObject.tag == "Medium")
            {
                s_agressorScore += 20;
                sendScoreToPlayer(1);
            }
            if (other.gameObject.tag == "High")
            {
                s_agressorScore += 30;
                sendScoreToPlayer(1);
            }
        }
        else if (other.gameObject.layer == 9 && this.gameObject.tag == "NurseBox")
        {
            if (other.gameObject.tag == "Low")
            {
                s_nurseScore += 10;
                sendScoreToPlayer(0);
            }
            if (other.gameObject.tag == "Medium")
            {
                s_nurseScore += 20;
                sendScoreToPlayer(0);
            }
            if (other.gameObject.tag == "High")
            {
                s_nurseScore += 30;
                sendScoreToPlayer(0);
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 9 && this.gameObject.tag == "AgressorBox")
        {
            if (s_agressorScore > 0)
            {
                if (other.gameObject.tag == "Low")
                {
                    s_agressorScore -= 10;
                    sendScoreToPlayer(1);
                }
                if (other.gameObject.tag == "Medium")
                {
                    s_agressorScore -= 20;
                    sendScoreToPlayer(1);
                }
                if (other.gameObject.tag == "High")
                {
                    s_agressorScore -= 30;
                    sendScoreToPlayer(1);
                }
            }
        }
        else if (other.gameObject.layer == 9 && this.gameObject.tag == "NurseBox")
        {
            if (s_nurseScore > 0)
            {
                if (other.gameObject.tag == "Low")
                {
                    s_nurseScore -= 10;
                    sendScoreToPlayer(0);
                }
                if (other.gameObject.tag == "Medium")
                {
                    s_nurseScore -= 20;
                    sendScoreToPlayer(0);
                }
                if (other.gameObject.tag == "High")
                {
                    s_nurseScore -= 30;
                    sendScoreToPlayer(0);
                }
            }
        }
    }

    public void sendScoreToPlayer(int activePlayer)
    {
        if (activePlayer == 0)
        {
            GameObject nurseBar = GameObject.Find("NurseBar");

            nurseBar.GetComponent<ScoreBar>().SetScore(s_nurseScore);
            Debug.Log("score nurse: " + s_nurseScore);
            player = GameObject.FindGameObjectWithTag("Nurse").transform.parent.transform.parent.gameObject;
            if (player.GetComponent<NetworkIdentity>().isLocalPlayer)
            {
                player.GetComponent<AssignAuth>().ExecuteCmdSendPlayerScore(s_nurseScore, 0);
            }
        }
        else if (activePlayer == 1)
        {
            GameObject agressorBar = GameObject.Find("AgressorBar");
            Debug.Log("score agressor: " + s_agressorScore);
            agressorBar.GetComponent<ScoreBar>().SetScore(s_agressorScore);
            player = GameObject.FindGameObjectWithTag("Agressor").transform.parent.transform.parent.gameObject;
            if (player.GetComponent<NetworkIdentity>().isLocalPlayer)
            {
                player.GetComponent<AssignAuth>().ExecuteCmdSendPlayerScore(s_agressorScore, 1);
            }
        }
    }
}
