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
        player.tag = "Nurse";
        visualRep.transform.gameObject.transform.GetChild(0).gameObject.SetActive(false);
        visualRep.transform.gameObject.transform.GetChild(1).gameObject.SetActive(false);
        visualRep.transform.GetChild(2).gameObject.SetActive(true);
        GameObject model = player.transform.GetChild(0).transform.GetChild(2).transform.GetChild(2).gameObject;
        model.transform.localPosition = new Vector3(0.035f, -0.72f, -0.12f);
        GameManager.CheckForTwoPlayers(1); // Tell gamemanager an agressor has been initialized.
    }

    [ClientRpc]
    public void RpcUpdateAgressor(GameObject player)
    {
        GameObject visualRep = player.transform.GetChild(0).transform.GetChild(2).gameObject;        
        player.tag = "Agressor";
        visualRep.transform.gameObject.transform.GetChild(0).gameObject.SetActive(false);
        visualRep.transform.GetChild(1).gameObject.SetActive(true);
        visualRep.transform.gameObject.transform.GetChild(2).gameObject.SetActive(false);
        GameObject model = player.transform.GetChild(0).transform.GetChild(2).transform.GetChild(1).gameObject;
        model.transform.localPosition = new Vector3(0.035f, -0.72f, -0.12f);
        GameManager.CheckForTwoPlayers(2); // Tell gamemanager an agressor has been initialized.
    }
}
