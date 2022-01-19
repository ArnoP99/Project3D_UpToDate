using Mirror;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreCounter : NetworkBehaviour
{

    int s_agressorScore = 0;
    int s_nurseScore = 0;

    int counterHighObjectsA = 0;
    int counterHighObjectsN = 0;
    int counterMediumObjectsA = 0;
    int counterMediumObjectsN = 0;
    int counterLowObjectsA = 0;
    int counterLowObjectsN = 0;

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
                counterLowObjectsA += 1;
                sendScoreToPlayer(1);
            }
            if (other.gameObject.tag == "Medium")
            {
                s_agressorScore += 20;
                counterMediumObjectsA += 1;
                sendScoreToPlayer(1);
            }
            if (other.gameObject.tag == "High")
            {
                s_agressorScore += 30;
                counterHighObjectsA += 1;
                sendScoreToPlayer(1);
            }
            if (other.gameObject.tag == "Ultra")
            {
                s_agressorScore += 100;
                sendScoreToPlayer(1);
            }
        }
        else if (other.gameObject.layer == 9 && this.gameObject.tag == "NurseBox")
        {
            if (other.gameObject.tag == "Low")
            {
                s_nurseScore += 10;
                counterLowObjectsN += 1;
                sendScoreToPlayer(0);
            }
            if (other.gameObject.tag == "Medium")
            {
                s_nurseScore += 20;
                counterMediumObjectsN += 1;
                sendScoreToPlayer(0);
            }
            if (other.gameObject.tag == "High")
            {
                s_nurseScore += 30;
                counterHighObjectsN += 1;
                sendScoreToPlayer(0);
            }
            if (other.gameObject.tag == "Ultra")
            {
                s_nurseScore += 100;
                sendScoreToPlayer(1);
            }

        }
    }
    private void OnTriggerExit(Collider other)
    {

        Debug.Log(other.gameObject);
        if (other.gameObject.layer == 9 && this.gameObject.tag == "AgressorBox")
        {
            if (s_agressorScore > 0)
            {
                if (other.gameObject.tag == "Low")
                {
                    s_agressorScore -= 10;
                    if (counterLowObjectsA > 0)
                    {
                        counterLowObjectsA -= 1;
                    }
                    sendScoreToPlayer(1);
                }
                if (other.gameObject.tag == "Medium")
                {
                    s_agressorScore -= 20;
                    if (counterMediumObjectsA > 0)
                    {
                        counterMediumObjectsA -= 1;
                    }
                    sendScoreToPlayer(1);
                }
                if (other.gameObject.tag == "High")
                {
                    s_agressorScore -= 30;
                    if (counterHighObjectsA > 0)
                    {
                        counterHighObjectsA -= 1;
                    }
                    sendScoreToPlayer(1);
                }
                if (other.gameObject.tag == "Ultra")
                {
                    s_agressorScore -= 100;
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
                    if (counterLowObjectsN > 0)
                    {
                        counterLowObjectsN -= 1;
                    }
                    sendScoreToPlayer(0);
                }
                if (other.gameObject.tag == "Medium")
                {
                    s_nurseScore -= 20;
                    if (counterLowObjectsN > 0)
                    {
                        counterMediumObjectsN -= 1;
                    }
                    sendScoreToPlayer(0);
                }
                if (other.gameObject.tag == "High")
                {
                    s_nurseScore -= 30;
                    if (counterLowObjectsN > 0)
                    {
                        counterHighObjectsN -= 1;
                    }
                    sendScoreToPlayer(0);
                }
                if (other.gameObject.tag == "Ultra")
                {
                    s_agressorScore -= 100;
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
            player = GameObject.FindGameObjectWithTag("Nurse").transform.parent.transform.parent.gameObject;
            if (player.GetComponent<NetworkIdentity>().isLocalPlayer)
            {
                player.GetComponent<AssignAuth>().ExecuteCmdSendPlayerScore(s_nurseScore, 0, counterHighObjectsN, counterMediumObjectsN, counterLowObjectsN);
            }
        }
        else if (activePlayer == 1)
        {
            GameObject agressorBar = GameObject.Find("AgressorBar");
            agressorBar.GetComponent<ScoreBar>().SetScore(s_agressorScore);
            player = GameObject.FindGameObjectWithTag("Agressor").transform.parent.transform.parent.gameObject;
            if (player.GetComponent<NetworkIdentity>().isLocalPlayer)
            {
                player.GetComponent<AssignAuth>().ExecuteCmdSendPlayerScore(s_agressorScore, 1, counterHighObjectsA, counterMediumObjectsA, counterLowObjectsA);
            }
        }
    }
}
