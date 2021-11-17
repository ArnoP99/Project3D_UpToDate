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
        if (player == isClient && player == isLocalPlayer)
        {
            CmdUpdateNurse(player);
        }
    }

    public void ExecuteAgressorChange(GameObject player)
    {
        Debug.Log("IsLocalPlayer A: " + (player == isClient));

        if (player == isClient && player == isLocalPlayer)
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
        GameObject visualRep = this.transform.GetChild(0).transform.GetChild(2).gameObject;

        Destroy(visualRep.transform.gameObject.transform.GetChild(0).gameObject);

        currentPos = this.transform.GetChild(0).position;
        currentPos.y = currentPos.y - 0.72f;
        currentPos.z = currentPos.y - 0.12f;
        currentRot.x = this.transform.GetChild(0).rotation.x;
        currentRot.z = this.transform.GetChild(0).rotation.z;
        currentRot.y = this.transform.GetChild(0).transform.GetChild(0).rotation.y;
        Instantiate(prefabNurse, currentPos, currentRot, this.transform);
        player.tag = "Nurse";
        GameManager.CheckForTwoPlayers(1); // Tell gamemanager an agressor has been initialized.
    }

    [ClientRpc]
    public void RpcUpdateAgressor(GameObject player)
    {
        GameObject visualRep = player.transform.GetChild(0).transform.GetChild(2).gameObject;

        Destroy(visualRep.transform.gameObject.transform.GetChild(0).gameObject);

        currentPos = player.transform.GetChild(0).position;
        currentPos.y = currentPos.y - 0.72f;
        currentPos.z = currentPos.y - 0.12f;
        currentRot.x = player.transform.GetChild(0).rotation.x;
        currentRot.z = player.transform.GetChild(0).rotation.z;
        currentRot.y = player.transform.GetChild(0).transform.GetChild(0).rotation.y;
        Instantiate(prefabAgressor, currentPos, currentRot, visualRep.transform);
        player.tag = "Agressor";
        GameManager.CheckForTwoPlayers(2); // Tell gamemanager an agressor has been initialized.
    }
}
