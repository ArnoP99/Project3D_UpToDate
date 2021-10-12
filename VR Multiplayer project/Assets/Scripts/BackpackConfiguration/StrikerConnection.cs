using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class StrikerConnection : NetworkBehaviour
{
    [SyncVar]
    private bool isConnectedToPlayer = false;

    //When the striker enters the player collider, the striker gets assigned to the player. The striker can't be assigned to another player thanks to the "isConnectedToPlayer" variable
    private void OnTriggerEnter(Collider other)
    {
        PlayerConfiguration player = other.transform.parent.GetComponent<PlayerConfiguration>();

        //Checks if the collider is a attached to a player
        if (player)
        {
            //Checks if the player isn't the server/PC
            if (player.PlayerID != 0)
            {
                //Checks if the blatser isn't connected to a player and if the player doesn't have a blaster assigned to it allready
                if (!isConnectedToPlayer && player.blaster == null)
                {
                    player.blaster = this.GetComponent<OptitrackRigidBody>();

                    isConnectedToPlayer = true;

                    this.transform.parent = player.transform; //Blaster becomes child of the player

                    Debug.Log(this.name + " connected to " + other.name);

                    //When the player is the local player the rigidbody ID of the blaster gets used to create a blastercontroller for registering input
                    if (player.isLocalPlayer)
                    {
                        ControllerConfiguration controllers = GameObject.Find("Controllers").GetComponent<ControllerConfiguration>();

                        controllers.InstantiateBlasterController(this.GetComponent<OptitrackRigidBody>().RigidBodyId);
                    }
                }
            }
        }
    }
}
