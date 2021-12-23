using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreCounter : MonoBehaviour
{
    int s_agressorScore = 0;


    int s_nurseScore = 0;



    void Start()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 9 && this.gameObject.tag == "AgressorBox")
        {
            s_agressorScore += 10;
            sendScoreToPlayer(s_agressorScore, "Agressor");
        }
        else if (other.gameObject.layer == 9 && this.gameObject.tag == "NurseBox")
        {
            s_nurseScore += 10;
            sendScoreToPlayer(s_nurseScore, "Nurse");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 9 && this.gameObject.tag == "AgressorBox")
        {
            if (s_agressorScore > 0)
            {
                s_agressorScore -= 10;
                sendScoreToPlayer(s_agressorScore, "Agressor");
            }
        }
        else if (other.gameObject.layer == 9 && this.gameObject.tag == "NurseBox")
        {
            if (s_nurseScore > 0)
            {
                s_nurseScore -= 10;
                sendScoreToPlayer(s_nurseScore, "Nurse");
            }
        }
    }

    private void sendScoreToPlayer(int score, string activePlayer)
    {
        GameObject player = GameObject.FindGameObjectWithTag(activePlayer);

        if (player.tag == "Nurse")
        {
            player.GetComponent<AssignAuth>().ExecuteCmdSendPlayerScore(score, 0);

        }
        else if (player.tag == "Agressor")
        {
            player.GetComponent<AssignAuth>().ExecuteCmdSendPlayerScore(score, 1);
        }
    }
}
