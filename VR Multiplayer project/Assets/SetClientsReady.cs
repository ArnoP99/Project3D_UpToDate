using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetClientsReady : NetworkBehaviour
{
    private void Start()
    {
        if (this == isServer)
        {
            RpcSetClientsReady();
        }
    }


    [ClientRpc]
    public void RpcSetClientsReady()
    {
        NetworkClient.ready = true;
    }

}
