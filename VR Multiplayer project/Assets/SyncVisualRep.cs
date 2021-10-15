using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SyncVisualRep : NetworkBehaviour
{
    [SyncVar] int visualRep = 0;
    void Start()
    {

    }


    void Update()
    {
        Debug.Log("New visual rep = " + visualRep);
    }

    public void SetVisualRepType(int x)
    {
        visualRep = x;
        Debug.Log("New visual rep = " + x);
    }

}
