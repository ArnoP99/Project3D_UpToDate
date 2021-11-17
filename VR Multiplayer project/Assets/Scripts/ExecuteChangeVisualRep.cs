using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExecuteChangeVisualRep : NetworkBehaviour
{
    [SerializeField] public GameObject prefabAgressor;
    [SerializeField] public GameObject prefabNurse;

    private Vector3 currentPos;
    private Quaternion currentRot;

    public void ExecuteNurseChange(GameObject player)
    {
        Debug.Log("IsLocalPlayer N: " + (player == isClient));
        if (player == isClient)
        {
            CmdUpdateNurse(player);
        }
    }

    public void ExecuteAgressorChange(GameObject player)
    {
        Debug.Log("IsLocalPlayer A: " + (player == isClient));

        if (player == isClient)
        {
            CmdUpdateAgressor(player);
        }
    }

    [Command(requiresAuthority = false)]
    void CmdUpdateNurse(GameObject player)
    {
        RpcUpdateNurse(player);
    }

    [Command(requiresAuthority = false)]
    void CmdUpdateAgressor(GameObject player)
    {
        RpcUpdateAgressor(player);
    }

    [ClientRpc]
    public void RpcUpdateNurse(GameObject player)
    {
        GameObject visualRep = player.transform.GetChild(0).transform.GetChild(2).gameObject;
        currentPos = player.transform.GetChild(0).position;
        currentPos = player.transform.GetChild(0).position;
        currentRot.x = player.transform.GetChild(0).rotation.x;
        currentRot.y = player.transform.GetChild(0).transform.GetChild(0).rotation.y;
        currentRot.z = player.transform.GetChild(0).rotation.z;
        player.tag = "Nurse";
        Destroy(visualRep.transform.gameObject.transform.GetChild(0).gameObject);
        Instantiate(prefabNurse, currentPos, currentRot, visualRep.transform);
        GameManager.CheckForTwoPlayers(1); // Tell gamemanager an agressor has been initialized.
    }

    [ClientRpc]
    public void RpcUpdateAgressor(GameObject player)
    {
        GameObject visualRep = player.transform.GetChild(0).transform.GetChild(2).gameObject;
        currentPos = player.transform.GetChild(0).position;
        currentRot.x = player.transform.GetChild(0).rotation.x;
        currentRot.y = player.transform.GetChild(0).transform.GetChild(0).rotation.y;
        currentRot.z = player.transform.GetChild(0).rotation.z;
        player.tag = "Agressor";
        Destroy(visualRep.transform.gameObject.transform.GetChild(0).gameObject);
        Instantiate(prefabAgressor, currentPos, currentRot, visualRep.transform);
        GameManager.CheckForTwoPlayers(2); // Tell gamemanager an agressor has been initialized.
    }
}
