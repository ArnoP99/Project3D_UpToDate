using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreCounter : MonoBehaviour
{
    int agressorScore = 0;
    GameObject AgressorScore;


    void Start()
    {
        AgressorScore = GameObject.Find("ScoreNumberAgressor");

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 9)
        {
            agressorScore += 10;
            AgressorScore.GetComponent<TextMeshPro>().text = agressorScore.ToString();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 9)
        {
            agressorScore -= 10;
            AgressorScore.GetComponent<TextMeshPro>().text = agressorScore.ToString();
        }
    }
}
