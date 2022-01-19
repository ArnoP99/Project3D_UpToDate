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
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 9)
        {
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
    }
}
