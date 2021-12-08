using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetClientsReady : NetworkBehaviour
{
    private void Start()
    {
        GameObject nurse = GameObject.FindGameObjectWithTag("Nurse");
        GameObject agressor = GameObject.FindGameObjectWithTag("Agressor");
        if(nurse == isClient && nurse == isLocalPlayer)
        {
            NetworkClient.Ready();
        }
        if (agressor == isClient && agressor == isLocalPlayer)
        {
            NetworkClient.Ready();
        }

        if(this == isServer)
        {
            NetworkServer.SpawnObjects();
        }
    }



}
