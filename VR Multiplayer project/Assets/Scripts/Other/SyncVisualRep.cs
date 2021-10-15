using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SyncVisualRep : NetworkBehaviour
{

    void Update()
    {
        string name = gameObject.name;
        CmdTest(name);
    }

    [Command(requiresAuthority = false)]
    public void CmdTest(string s)
    {
        Debug.Log("Test" + s);
        
       
    }

}
