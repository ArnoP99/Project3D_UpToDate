using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssignAuth : NetworkBehaviour
{

    // Function where we can check if player isClient and isLocalPlayer and not isServer before executing CmdAssignAuthority
    public void ExecuteCmdAssignAuthority(NetworkIdentity objectID)
    {
        //Debug.Log("AssAuth Player: " + this);

        //Debug.Log("AssAuth Player == IsClient: " + (this == isClient));
        //Debug.Log("AssAuth Player == IsServer: " + (this == isServer));
        //Debug.Log("AssAuth Player == IsLocalPlayer: " + (this == isLocalPlayer));

        //Debug.Log("AssAuth Other GO NetID: " + objectID);
        //Debug.Log("AssAuth player NetID: " + this.GetComponent<NetworkIdentity>());

        if (this == isClient && this != isServer && this == isLocalPlayer)
        {
            CmdAssignAuthority(objectID, this.GetComponent<NetworkIdentity>());
        }
    }

    // Function where we can check if player isClient and isLocalPlayer and not isServer before executing CmdRemoveAuthority
    public void ExecuteCmdRemoveAuthority(NetworkIdentity objectID)
    {
        //Debug.Log("RemAuth Player: " + this);

        //Debug.Log("RemAuth Player == IsClient: " + (this == isClient));
        //Debug.Log("RemAuth Player == IsServer: " + (this == isServer));
        //Debug.Log("RemAuth Player == IsLocalPlayer: " + (this == isLocalPlayer));

        //Debug.Log("RemAuth Other GO NetID: " + objectID);
        //Debug.Log("RemAuth player NetID: " + this.GetComponent<NetworkIdentity>());

        if (this == isClient && this != isServer && this == isLocalPlayer)
        {
            CmdRemoveAuthority(objectID);
        }
    }


    [Command (requiresAuthority = false)]
    public void CmdAssignAuthority(NetworkIdentity objectID, NetworkIdentity playerID)
    {
        //Debug.Log("Authority Assigned to: " + this);
        objectID.AssignClientAuthority(this.GetComponent<NetworkIdentity>().connectionToClient);
    }

    [Command (requiresAuthority = false)]
    public void CmdRemoveAuthority(NetworkIdentity objectID)
    {
        //Debug.Log("Authority Removed from object.");
        objectID.RemoveClientAuthority();
    }
}
