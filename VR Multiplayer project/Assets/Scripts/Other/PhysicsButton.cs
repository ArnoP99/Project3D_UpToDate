//using Mirror;
//using System;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.Events;
//using UnityEngine.SceneManagement;

//public class PhysicsButton : NetworkBehaviour
//{
    

//    void Start()
//    {
//        startPos = transform.localPosition;
//        joint = GetComponent<ConfigurableJoint>();
//    }

//    private void OnCollisionEnter(Collision collision)
//    {

//        Pressed();

//        currentPos = collision.transform.parent.transform.parent.position;

//        if (collision.gameObject.tag == "RightController" || collision.gameObject.tag == "LeftController")
//        {
//            GameObject player = collision.gameObject.transform.parent.transform.parent.transform.parent.gameObject;

//            if (gameObject.tag == "AgressorButton")
//            {
//                Debug.Log("IsLocalPlayer A: " + (player == isClient));

//                if (player == isClient)
//                {
//                    CmdUpdateAgressor(player);
//                }
//            }
//            if (gameObject.tag == "NurseButton")
//            {
//                Debug.Log("IsLocalPlayer N: " + (player == isClient));
//                if (player == isClient)
//                {
//                    CmdUpdateNurse(player);
//                }
//            }
//            //if (gameObject.tag == "SceneButton")
//            //{
//            //    SceneManager.LoadScene("ZiekenhuisKamer");
//            //    Scene ziekenHuisKamer = SceneManager.GetSceneByName("ZiekenhuisKamer");
//            //}
//        }
//    }

//    private void OnCollisionExit(Collision collision)
//    {
//        Released();
//    }

//    private float GetValue()
//    {
//        var value = Vector3.Distance(startPos, transform.localPosition) / joint.linearLimit.limit;

//        if (Math.Abs(value) < deadZone)
//        {
//            value = 0;
//        }
//        return Mathf.Clamp(value, -1f, 1f);
//    }

//    private void Pressed()
//    {
//        isPressed = true;
//        onPressed.Invoke();
//        Debug.Log("Pressed");
//    }

//    private void Released()
//    {
//        isPressed = false;
//        onReleased.Invoke();
//        Debug.Log("Released");
//    }

//    //[Command(requiresAuthority = false)]
//    //void CmdMessageTest(GameObject player)
//    //{
//    //    Debug.Log("This is a message run from the server, initiated by the player: " + player.name);
//    //}

//    //[ClientRpc(includeOwner = false)]
//    //public void RpcTest()
//    //{
//    //    Debug.Log("Message from Server To Client");
//    //}

//    //[TargetRpc]
//    //public void TargetTest(NetworkConnection target)
//    //{
//    //    Debug.Log("server to specific target");
//    //}

//    [Command(requiresAuthority = false)]
//    void CmdUpdateNurse(GameObject player)
//    {
//        RpcUpdateNurse(player);
//    }

//    [Command(requiresAuthority = false)]
//    void CmdUpdateAgressor(GameObject player)
//    {
//        RpcUpdateAgressor(player);
//    }

//    [ClientRpc]
//    public void RpcUpdateNurse(GameObject player)
//    {
//        GameObject visualRep = player.transform.GetChild(0).transform.GetChild(2).gameObject;
//        player.tag = "Nurse";
//        Destroy(visualRep.transform.gameObject.transform.GetChild(0).gameObject);
//        Instantiate(prefabNurse, currentPos, Quaternion.identity, visualRep.transform);
//        GameManager.CheckForTwoPlayers(1); // Tell gamemanager an agressor has been initialized.
//    }

//    [ClientRpc]
//    public void RpcUpdateAgressor(GameObject player)
//    {
//        GameObject visualRep = player.transform.GetChild(0).transform.GetChild(2).gameObject;
//        player.tag = "Agressor";
//        Destroy(visualRep.transform.gameObject.transform.GetChild(0).gameObject);
//        Instantiate(prefabAgressor, currentPos, player.transform.GetChild(0).transform.GetChild(0).rotation, visualRep.transform);
//        GameManager.CheckForTwoPlayers(2); // Tell gamemanager an agressor has been initialized.
//    }
//}
