using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using System;

public class SyncRotation : NetworkBehaviour
{
    GameObject playerCamera;
    GameObject visualRepresentation;
    GameObject textPlayer;

    Vector3 rot;
    Vector3 rot1;
    Vector3 rot2;
    Vector3 rot3;

    private void Start()
    {
        if (this.isLocalPlayer)
        {
            playerCamera = gameObject.transform.GetChild(0).transform.GetChild(0).gameObject;
            visualRepresentation = gameObject.transform.GetChild(0).transform.GetChild(2).gameObject;
            textPlayer = gameObject.transform.GetChild(0).transform.GetChild(3).gameObject;
        }
    }


    void Update()
    {
        rot = new Vector3(0, playerCamera.transform.eulerAngles.y, 0);
        rot1 = new Vector3(0, 0, 0);
        rot2 = new Vector3(0, 120, 0);
        rot3 = new Vector3(0, 240, 0);

        visualRepresentation.transform.eulerAngles = rot;

        if (playerCamera.transform.eulerAngles.y < 60 || playerCamera.transform.eulerAngles.y > 300)
        {
            textPlayer.transform.eulerAngles = rot1;
        }
        else if (playerCamera.transform.eulerAngles.y > 60 && playerCamera.transform.eulerAngles.y < 180)
        {
            textPlayer.transform.eulerAngles = rot2;
        }
        else if (playerCamera.transform.eulerAngles.y > 180 && playerCamera.transform.eulerAngles.y < 300)
        {
            textPlayer.transform.eulerAngles = rot3;
        }
    }
}
