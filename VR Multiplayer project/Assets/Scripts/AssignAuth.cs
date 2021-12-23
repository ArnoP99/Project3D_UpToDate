using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssignAuth : NetworkBehaviour
{
    private int nurseScore = 0;
    private int agressorScore = 0;



    public void ExecuteCmdHandGoesPoof(int hand, GameObject player)
    {
        if (this == isClient && this != isServer && this == isLocalPlayer && hand == 0)
        {
            CmdHandGoesPoof(0, player);
        }
        else if (this == isClient && this != isServer && this == isLocalPlayer && hand == 1)
        {
            CmdHandGoesPoof(1, player);
        }
    }

    public void ExecuteCmdHandComesBack(int hand, GameObject player)
    {
        if (this == isClient && this != isServer && this == isLocalPlayer && hand == 0)
        {
            CmdHandComesBack(0, player);
        }
        else if (this == isClient && this != isServer && this == isLocalPlayer && hand == 1)
        {
            CmdHandComesBack(1, player);
        }
    }
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

    public void ExecuteCmdSendPlayerScore(int score, int player)
    {
        if (this == isClient && this != isServer && this == isLocalPlayer)
        {
            CmdSendPlayerScore(score, player);
        }
    }

    [Command(requiresAuthority = false)]
    public void CmdSendPlayerScore(int score, int player)
    {
        NetworkIdentity nurseID = GameObject.FindGameObjectWithTag("Nurse").transform.parent.transform.parent.gameObject.GetComponent<NetworkIdentity>();
        NetworkIdentity agressorID = GameObject.FindGameObjectWithTag("Agressor").transform.parent.transform.parent.gameObject.GetComponent<NetworkIdentity>();

        if (player == 0)
        {
            GameObject nurseBar = GameObject.Find("NurseBar");
            nurseBar.GetComponent<ScoreBar>().SetScore(score);
            nurseScore = score;
            Debug.Log("Nurse score: " + nurseScore);
            if (this.isServer)
            {
                TargetSendNurseScore(agressorID.connectionToClient, nurseScore);
            }
        }
        else if (player == 1)
        {
            GameObject agressorBar = GameObject.Find("AgressorBar");
            agressorBar.GetComponent<ScoreBar>().SetScore(score);
            agressorScore = score;
            Debug.Log("Agressor score: " + agressorScore);
            if (this.isServer)
            {
                TargetSendAgressorScore(nurseID.connectionToClient, agressorScore);
            }
        }
    }

    [TargetRpc]
    public void TargetSendNurseScore(NetworkConnection playerConnection, int otherPlayerScore)
    {
        GameObject nurseBar = GameObject.Find("NurseBar");

        Debug.Log("NurseScore" + nurseScore);

        nurseBar.GetComponent<ScoreBar>().SetScore(nurseScore);
    }

    [TargetRpc]
    public void TargetSendAgressorScore(NetworkConnection playerConnection, int otherPlayerScore)
    {

        GameObject agressorBar = GameObject.Find("AgressorBar");


        Debug.Log("AgressorScore" + agressorScore);


        agressorBar.GetComponent<ScoreBar>().SetScore(agressorScore);
    }

    [Command(requiresAuthority = false)]
    public void CmdAssignAuthority(NetworkIdentity objectID, NetworkIdentity playerID)
    {
        //Debug.Log("Authority Assigned to: " + this);
        objectID.AssignClientAuthority(this.GetComponent<NetworkIdentity>().connectionToClient);
    }

    [Command(requiresAuthority = false)]
    public void CmdRemoveAuthority(NetworkIdentity objectID)
    {
        //Debug.Log("Authority Removed from object.");
        objectID.RemoveClientAuthority();
    }

    [Command(requiresAuthority = false)]
    public void CmdHandGoesPoof(int hand, GameObject player)
    {
        if (hand == 0)
        {
            player.transform.GetChild(0).transform.GetChild(1).transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(false);
            RpcHandGoesPoof(0, player);
        }
        else if (hand == 1)
        {
            player.transform.GetChild(0).transform.GetChild(1).transform.GetChild(1).transform.GetChild(0).gameObject.SetActive(false);
            RpcHandGoesPoof(1, player);
        }
    }

    [Command(requiresAuthority = false)]
    public void CmdHandComesBack(int hand, GameObject player)
    {
        if (hand == 0)
        {
            player.transform.GetChild(0).transform.GetChild(1).transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(true);
            RpcHandComesBack(0, player);
        }
        else if (hand == 1)
        {
            player.transform.GetChild(0).transform.GetChild(1).transform.GetChild(1).transform.GetChild(0).gameObject.SetActive(true);
            RpcHandComesBack(1, player);
        }

    }

    [ClientRpc]
    public void RpcHandGoesPoof(int hand, GameObject player)
    {

        if (hand == 0)
        {
            player.transform.GetChild(0).transform.GetChild(1).transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(false);
        }
        else if (hand == 1)
        {
            player.transform.GetChild(0).transform.GetChild(1).transform.GetChild(1).transform.GetChild(0).gameObject.SetActive(false);
        }
    }

    [ClientRpc]
    public void RpcHandComesBack(int hand, GameObject player)
    {
        if (hand == 0)
        {
            player.transform.GetChild(0).transform.GetChild(1).transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(true);
        }
        else if (hand == 1)
        {
            player.transform.GetChild(0).transform.GetChild(1).transform.GetChild(1).transform.GetChild(0).gameObject.SetActive(true);
        }
    }
}
