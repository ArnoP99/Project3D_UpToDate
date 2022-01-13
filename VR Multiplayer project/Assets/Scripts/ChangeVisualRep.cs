using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeVisualRep : NetworkBehaviour
{
    

    public void Start()
    {
        //pointerAgressor = GameObject.FindGameObjectWithTag("PointerAgressor");
        //pointerAgressor.SetActive(false);
        //pointerNurse = GameObject.FindGameObjectWithTag("PointerNurse");
        //pointerNurse.SetActive(false);
        
    }
    // Start is called before the first frame update
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "NurseButton" || other.gameObject.tag == "AgressorButton")
        {
            GameObject player = this.transform.parent.transform.parent.transform.parent.gameObject;

            if (other.gameObject.tag == "AgressorButton" && player.GetComponent<NetworkIdentity>().isLocalPlayer)
            {
                player.GetComponent<ExecuteChangeVisualRep>().ExecuteAgressorChange(player);
                //GameObject.FindGameObjectWithTag("PointerAgressor").SetActive(true);
                
            }
            if (other.gameObject.tag == "NurseButton" && player.GetComponent<NetworkIdentity>().isLocalPlayer)
            {
                player.GetComponent<ExecuteChangeVisualRep>().ExecuteNurseChange(player);
                //GameObject.FindGameObjectWithTag("PointerNurse").SetActive(true);
                
            }
        }
    }
}
