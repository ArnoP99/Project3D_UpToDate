using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExecuteChangeVisualRep : NetworkBehaviour
{
    [SerializeField] public GameObject prefabAgressor;
    [SerializeField] public GameObject prefabNurse;

    private Vector3 currentPos;

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
        player.tag = "Nurse";
        Destroy(visualRep.transform.gameObject.transform.GetChild(0).gameObject);
        Instantiate(prefabNurse, currentPos, Quaternion.identity, visualRep.transform);
        GameManager.CheckForTwoPlayers(1); // Tell gamemanager an agressor has been initialized.
    }

    [ClientRpc]
    public void RpcUpdateAgressor(GameObject player)
    {
        GameObject visualRep = player.transform.GetChild(0).transform.GetChild(2).gameObject;
        player.tag = "Agressor";
        Destroy(visualRep.transform.gameObject.transform.GetChild(0).gameObject);
        Instantiate(prefabAgressor, currentPos, player.transform.GetChild(0).transform.GetChild(0).rotation, visualRep.transform);
        GameManager.CheckForTwoPlayers(2); // Tell gamemanager an agressor has been initialized.
    }
}
