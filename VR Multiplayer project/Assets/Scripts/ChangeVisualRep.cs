using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeVisualRep : MonoBehaviour
{
    public void Start()
    {
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "NurseButton" || other.gameObject.tag == "AgressorButton")
        {
            GameObject player = this.transform.parent.transform.parent.transform.parent.gameObject;

            if (other.gameObject.tag == "AgressorButton")
            {
                player.GetComponent<ExecuteChangeVisualRep>().ExecuteAgressorChange(player);
            }
            if (other.gameObject.tag == "NurseButton")
            {
                player.GetComponent<ExecuteChangeVisualRep>().ExecuteNurseChange(player);
            }
        }
    }
}
