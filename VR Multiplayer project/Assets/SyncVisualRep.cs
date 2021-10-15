using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SyncVisualRep : NetworkBehaviour
{

    void Update()
    {
        string name = gameObject.transform.parent.transform.parent.name;
        Testcmd(name);
    }

    [Command(requiresAuthority = false)]
    public void Testcmd(string s)
    {
        Debug.Log("Test" + s);
        
       
    }

}
