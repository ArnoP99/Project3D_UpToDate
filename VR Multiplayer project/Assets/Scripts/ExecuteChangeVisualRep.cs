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

    MeshRenderer pointerNurse;
    MeshRenderer pointerAgressor;

    public void Start()
    {
        pointerAgressor = GameObject.FindGameObjectWithTag("PointerAgressor").GetComponent<MeshRenderer>();
        pointerNurse = GameObject.FindGameObjectWithTag("PointerNurse").GetComponent<MeshRenderer>();
        pointerAgressor.enabled = false;
        pointerNurse.enabled = false;
    }
    public void ExecuteNurseChange(GameObject player)
    {
        if (player.GetComponent<NetworkIdentity>().isLocalPlayer)
        {
            pointerNurse.enabled = true;
            pointerAgressor.enabled = false;
        }
        if (player == isClient)
        {
            CmdUpdateNurse(player);
        }
    }

    public void ExecuteAgressorChange(GameObject player)
    {
        if (player.GetComponent<NetworkIdentity>().isLocalPlayer)
        {
            pointerNurse.enabled = false;
            pointerAgressor.enabled = true;
        }
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
        visualRep.tag = "Nurse";
        visualRep.transform.gameObject.transform.GetChild(0).gameObject.SetActive(false);
        visualRep.transform.gameObject.transform.GetChild(1).gameObject.SetActive(false);
        visualRep.transform.GetChild(2).gameObject.SetActive(true);
        GameObject model = player.transform.GetChild(0).transform.GetChild(2).transform.GetChild(2).gameObject;
        model.transform.localPosition = new Vector3(0.035f, -0.72f, -0.12f);

    }

    [ClientRpc]
    public void RpcUpdateAgressor(GameObject player)
    {
        GameObject visualRep = player.transform.GetChild(0).transform.GetChild(2).gameObject;
        visualRep.tag = "Agressor";
        visualRep.transform.gameObject.transform.GetChild(0).gameObject.SetActive(false);
        visualRep.transform.GetChild(1).gameObject.SetActive(true);
        visualRep.transform.gameObject.transform.GetChild(2).gameObject.SetActive(false);
        GameObject model = player.transform.GetChild(0).transform.GetChild(2).transform.GetChild(1).gameObject;
        model.transform.localPosition = new Vector3(0.035f, -0.72f, -0.12f);

    }
}
