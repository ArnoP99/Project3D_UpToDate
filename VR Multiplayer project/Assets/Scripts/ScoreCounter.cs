using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreCounter : MonoBehaviour
{
    int s_agressorScore = 0;
    GameObject agressorScore;

    int s_nurseScore = 0;
    GameObject nurseScore;


    void Start()
    {
        agressorScore = GameObject.Find("ScoreNumberAgressor");
        nurseScore = GameObject.Find("ScoreNumberNurse");

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 9 && this.gameObject.tag == "AgressorBox")
        {
            s_agressorScore += 10;
            agressorScore.GetComponent<TextMeshPro>().text = s_agressorScore.ToString();
        }
        else if (other.gameObject.layer == 9 && this.gameObject.tag == "NurseBox")
        {
            s_nurseScore += 10;
            nurseScore.GetComponent<TextMeshPro>().text = s_nurseScore.ToString();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 9 && this.gameObject.tag == "AgressorBox")
        {
            if (s_agressorScore > 0)
            {
                s_agressorScore -= 10;
                agressorScore.GetComponent<TextMeshPro>().text = s_agressorScore.ToString();
            }
        }
        else if (other.gameObject.layer == 9 && this.gameObject.tag == "NurseBox")
        {
            if (s_nurseScore > 0)
            {
                s_nurseScore -= 10;
                nurseScore.GetComponent<TextMeshPro>().text = s_nurseScore.ToString();
            }
        }
    }
}
