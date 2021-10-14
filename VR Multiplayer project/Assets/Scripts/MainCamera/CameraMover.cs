using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraMover : MonoBehaviour
{
    protected Vector3 focusTarget; //This is the point on which the camera focusses

    [SerializeField]
    protected Transform playerParent; //Parent object of all players

    [SerializeField]
    protected int bottomBorder = 10; //Minimum height in degrees for camera to look at focus point
    [SerializeField]
    protected int upperBorder = 45; //maximum height in degrees for camera to look at focus point

    [SerializeField]
    protected int closeBorder = 10; //Minimum distance from camera to focus point
    [SerializeField]
    protected int farBorder = 50; //Maximum distance from camera to focus point

    [SerializeField]
    protected float moveSpeedInDegrees = 10;
    [SerializeField]
    protected float scrollspeed = 0.1f;

    void Start()
    {
        DontDestroyOnLoad(gameObject);
        focusTarget = new Vector3(0, 0, 0); //Starting focus po_int is the origin
    }

    void Update()
    {
        //When left arrow pressed move move left
        if (UnityEngine.Input.GetKey(UnityEngine.KeyCode.LeftArrow))
        {
            transform.RotateAround(focusTarget, Vector3.up, moveSpeedInDegrees * Time.deltaTime);
        }

        //When right arrow pressed move move right
        if (UnityEngine.Input.GetKey(UnityEngine.KeyCode.RightArrow))
        {
            transform.RotateAround(focusTarget, Vector3.down, moveSpeedInDegrees * Time.deltaTime);
        }

        //When up arrow pressed move move up
        if (UnityEngine.Input.GetKey(UnityEngine.KeyCode.UpArrow))
        {
            if (transform.eulerAngles.x < upperBorder)
            {
                transform.RotateAround(focusTarget, this.transform.right, moveSpeedInDegrees * Time.deltaTime);
            }
        }

        //When down arrow pressed move move down
        if (UnityEngine.Input.GetKey(UnityEngine.KeyCode.DownArrow))
        {
            if (transform.eulerAngles.x > bottomBorder)
            {
                transform.RotateAround(focusTarget, -this.transform.right, moveSpeedInDegrees * Time.deltaTime);
            }
        }


        
        float scrollDelta = UnityEngine.Input.mouseScrollDelta.y; //Get the value of howmuch is scrolled since the last frame


        //Move the camera closer of father away from the focus point according to the scrolldelta value
        if(scrollDelta != 0)
        {
            Vector3 cameraToTargetVector = this.transform.position - focusTarget;

            if (cameraToTargetVector.magnitude > closeBorder && scrollDelta > 0)
            {
                this.transform.position -= scrollDelta * cameraToTargetVector * scrollspeed;
            }
            if (cameraToTargetVector.magnitude < farBorder && scrollDelta < 0)
            {
                this.transform.position -= scrollDelta * cameraToTargetVector * scrollspeed;
            }
        }
    }

    public void ChangeFocusTarget(int buttonNumber)
    {
        Debug.Log(buttonNumber);

        //When a button is presed change to that player as new focus point, when the player allready is the focuspoint take the origin as focus point
        if (buttonNumber >= 0)
        {
            int playerID = buttonNumber + 1;

            for (int i = 0; i < playerParent.childCount; i++)
            {
                if (playerParent.GetChild(i).GetComponent<PlayerConfiguration>().PlayerID == playerID)
                {
                    this.transform.parent = playerParent.GetChild(i).GetChild(0);

                    Vector3 oldTargetToNewTargetVector = focusTarget + playerParent.GetChild(i).GetChild(0).position;
                    this.transform.position += oldTargetToNewTargetVector;
                    focusTarget = playerParent.GetChild(i).GetChild(0).position;
                }
            }
        }
        else
        {
            this.transform.parent = null;

            Vector3 oldTargetToOriginVector = new Vector3(0, 0, 0) - focusTarget;
            this.transform.position += oldTargetToOriginVector;
            focusTarget = new Vector3(0, 0, 0);
        }
    }
}
