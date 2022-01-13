using System.Collections;
using UnityEngine;
using Mirror;
using UnityEngine.SpatialTracking;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class PlayerConfiguration : NetworkBehaviour
{
    public Camera myCamera;
    public OptitrackHmd optitrackrigidHmd;
    public OptitrackStreamingClient optitrackClient;
    public ControllerConfiguration controllerConfiguration;
    public ControllersToHMDLocal controllersToHMDLocal;
    public SyncRotation syncRotation;
    public AudioSource audioSource;

    public OptitrackRigidBody blaster;

    [SyncVar]
    public int PlayerID;

    [SyncVar]
    public int PlayerRigidbodyID;

    public TrackedPoseDriver TrackedPoseDriverPlayerCamera;
    public TrackedPoseDriver TrackedPoseDriverLeftController;
    public TrackedPoseDriver TrackedPoseDriverRightController;

    public XRController XRControllerLeft;
    public XRController XRControllerRight;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(WaitOneFrame()); //Wait 1 frame before instantiating player
    }

    IEnumerator WaitOneFrame()
    {
        yield return new WaitForEndOfFrame();

        SetUpPlayer();
    }

    protected void SetUpPlayer()
    {
        optitrackrigidHmd = this.GetComponentInChildren<OptitrackHmd>();
        TrackedPoseDriverPlayerCamera = this.transform.GetChild(0).transform.GetChild(0).GetComponent<TrackedPoseDriver>();
        myCamera = this.GetComponentInChildren<Camera>();
        optitrackClient = GameObject.Find("OptitrackClient").GetComponent<OptitrackStreamingClient>();
        controllerConfiguration = GameObject.Find("Controllers").GetComponent<ControllerConfiguration>();
        controllersToHMDLocal = this.GetComponentInChildren<ControllersToHMDLocal>();
        syncRotation = this.GetComponentInChildren<SyncRotation>();
        audioSource = this.GetComponentInChildren<AudioSource>();


        // for hands, instantiate them with autohority after instantiating player (delete them from prefab, ...)
        if (isLocalPlayer)
        {
            //When it is the local player and isn't the server/PC enable the camera, the tracked pose driver of the camera
            if (NetworkConfiguration.GameSettings.ID != 0)
            {
                Debug.Log("LocalPlayer && != server");
                TrackedPoseDriverLeftController = this.transform.GetChild(0).transform.GetChild(1).transform.GetChild(0).GetComponent<TrackedPoseDriver>();
                TrackedPoseDriverRightController = this.transform.GetChild(0).transform.GetChild(1).transform.GetChild(1).GetComponent<TrackedPoseDriver>();
                XRControllerLeft = this.transform.GetChild(0).transform.GetChild(1).transform.GetChild(0).GetComponent<XRController>();
                XRControllerRight = this.transform.GetChild(0).transform.GetChild(1).transform.GetChild(1).GetComponent<XRController>();
                syncRotation.enabled = true;
                TrackedPoseDriverPlayerCamera.enabled = true;
                TrackedPoseDriverLeftController.enabled = true;
                TrackedPoseDriverRightController.enabled = true;
                XRControllerLeft.enabled = true;
                XRControllerRight.enabled = true;
                myCamera.enabled = true;
                audioSource.enabled = false;
                myCamera.GetComponent<AudioListener>().enabled = true;
                this.transform.GetChild(0).transform.GetChild(2).transform.GetChild(1).gameObject.SetActive(false);
                this.transform.GetChild(0).transform.GetChild(2).transform.GetChild(2).gameObject.SetActive(false);
                this.transform.GetChild(0).transform.GetChild(2).transform.GetChild(1).gameObject.SetActive(false);
                this.transform.GetChild(0).transform.GetChild(2).transform.GetChild(2).gameObject.SetActive(false);
                this.transform.GetChild(0).transform.GetChild(3).gameObject.SetActive(false);
                
            }
            //When it is the local player and it is the server/PC enable the main camera
            else
            {
                Debug.Log("LocalPlayer && == server");
                syncRotation.enabled = false;
                controllersToHMDLocal.enabled = false;
                myCamera.enabled = false;
                audioSource.enabled = false;
                myCamera.GetComponent<AudioListener>().enabled = false;
                GameObject.Find("Main camera").GetComponent<Camera>().enabled = true;
                GameObject.Find("Main camera").GetComponent<AudioListener>().enabled = true;
                gameObject.tag = "Server";
                
            }

            sentPlayerInfo(NetworkConfiguration.GameSettings.ID, NetworkConfiguration.GameSettings.Rigidbodybody); //Share player info with other devices
            optitrackrigidHmd.RigidBodyId = NetworkConfiguration.GameSettings.Rigidbodybody; //Set rigidbody ID
            optitrackrigidHmd.StreamingClient = optitrackClient;
        }
        //When it isn't the local player dissable camera and audiolistener
        else
        {
            XRControllerLeft = this.transform.GetChild(0).transform.GetChild(1).transform.GetChild(0).GetComponent<XRController>();
            XRControllerRight = this.transform.GetChild(0).transform.GetChild(1).transform.GetChild(1).GetComponent<XRController>();
            TrackedPoseDriverLeftController = this.transform.GetChild(0).transform.GetChild(1).transform.GetChild(0).GetComponent<TrackedPoseDriver>();
            TrackedPoseDriverRightController = this.transform.GetChild(0).transform.GetChild(1).transform.GetChild(1).GetComponent<TrackedPoseDriver>();
            Debug.Log("Not LocalPlayer");
            syncRotation.enabled = false;
            TrackedPoseDriverPlayerCamera.enabled = false;
            TrackedPoseDriverLeftController.enabled = false;
            TrackedPoseDriverRightController.enabled = false;
            XRControllerLeft.enabled = false;
            XRControllerRight.enabled = false;
            myCamera.enabled = false;
            audioSource.enabled = true;
            myCamera.GetComponent<AudioListener>().enabled = false;
            this.transform.GetChild(0).transform.GetChild(3).gameObject.SetActive(false);
            this.transform.GetChild(0).transform.GetChild(2).transform.GetChild(1).gameObject.SetActive(false);
            this.transform.GetChild(0).transform.GetChild(2).transform.GetChild(2).gameObject.SetActive(false);
        }

        //Make server/PC player invisible and make hands invisible
        if (PlayerID == 0)
        {
            this.transform.GetChild(0).transform.GetChild(2).transform.GetChild(0).gameObject.SetActive(false);
            this.transform.GetChild(0).transform.GetChild(2).transform.GetChild(1).gameObject.SetActive(false);
            this.transform.GetChild(0).transform.GetChild(2).transform.GetChild(2).gameObject.SetActive(false);
            this.transform.GetChild(0).transform.GetChild(1).transform.GetChild(0).gameObject.SetActive(false);
            this.transform.GetChild(0).transform.GetChild(1).transform.GetChild(1).gameObject.SetActive(false);
            this.transform.GetChild(0).transform.GetChild(3).gameObject.SetActive(false);
            gameObject.tag = "Server";
        }
        this.transform.parent = GameObject.Find("Players").transform; //Set 'Players" gameobject as parent

        optitrackClient.LocalAddress = NetworkConfiguration.GameSettings.IPAdress; //Set IP adress
        optitrackClient.enabled = true;

        //When we use the HPReverb Controllers and this isn't the server/PC instantiate the reverb controllers
        if (controllerConfiguration.typeOfController == ControllerConfiguration.TypeOfController.ReverbControllers && PlayerID != 0)
        {
            InstantiateReverbControllers();
        }
    }


    //Receive player ID and rigidbody
    [ClientRpc]
    public void receivePlayerID(int playerid, int playerRigidbodyid)
    {

    }

    //Sent player ID and rigidbody
    [Command]
    public void sentPlayerInfo(int playerid, int playerRigidbodyid)
    {
        PlayerID = playerid;
        PlayerRigidbodyID = playerRigidbodyid;
    }

    //Set controllers active and enable the player input component
    public void InstantiateReverbControllers()
    {
        GameObject leftController = this.transform.GetChild(0).GetChild(1).GetChild(0).gameObject;
        leftController.SetActive(true);
        GameObject rightController = this.transform.GetChild(0).GetChild(1).GetChild(1).gameObject;
        rightController.SetActive(true);

        if (isLocalPlayer)
        {
            this.GetComponent<PlayerInput>().enabled = true;
        }
    }
}
