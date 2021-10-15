using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SyncVisualRep : NetworkBehaviour
{

    void Update()
    {
        string name = gameObject.name;
        RpcTest(name);
    }

    [ClientRpc]
    public void RpcTest(string s)
    {
        Debug.Log("Test" + s);              
    }
}
