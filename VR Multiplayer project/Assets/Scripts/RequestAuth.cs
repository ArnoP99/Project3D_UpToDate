using Mirror;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RequestAuth : MonoBehaviour
{
    GameObject player;

    private void Start()
    {
        player = this.transform.parent.transform.parent.transform.parent.gameObject;
        //Debug.Log("ReqAuth Player: " + player);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 9)
        {
            if (this.tag == "LeftController")
            {
                player.GetComponent<AssignAuth>().ExecuteCmdHandGoesPoof(0, player);
            }
            else if (this.tag == "RightController")
            {
                player.GetComponent<AssignAuth>().ExecuteCmdHandGoesPoof(1, player);
            }
            try
            {
                player.GetComponent<AssignAuth>().ExecuteCmdRemoveAuthority(other.GetComponent<NetworkIdentity>());
            }
            catch (Exception ex)
            {
                Debug.Log("Object did not have any authority. " + ex);
            }
            player.GetComponent<AssignAuth>().ExecuteCmdAssignAuthority(other.GetComponent<NetworkIdentity>());
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (this.tag == "LeftController")
        {
            player.GetComponent<AssignAuth>().ExecuteCmdHandComesBack(0, player);
        }
        else if (this.tag == "RightController")
        {
            player.GetComponent<AssignAuth>().ExecuteCmdHandComesBack(1, player);
        }
    }
}
