using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerConfiguration : MonoBehaviour
{
    //Possible controller types
    public enum TypeOfController
    {
        None,
        ReverbControllers,
        Striker
    }

    //Used controller type
    public TypeOfController typeOfController = TypeOfController.ReverbControllers;

    protected OptitrackStreamingClient optitrackStreamingClient;

    [SerializeField]
    public GameObject HPReverbLeftHand; //Prefab for left hand
    [SerializeField]
    public GameObject HPReverbRightHand; //Prefab for right hand

    [SerializeField]
    protected BlasterOperator blasterController; //Prefab for blaster input
    [SerializeField]
    protected OptitrackRigidBody blasterRigidBody; //Prefab for blaster potion

    [SerializeField]
    protected string[] blasterMacAdresses; // All possible blaster mac adresses
    [SerializeField]
    protected int[] blasterRigidBodyIDs; //All possible blaster rigidbody ID's corresponding with the blaster mac adresses

    protected Dictionary<int, string> blasterMacAdressesAndRigidBodyIDs = new Dictionary<int, string>(); //Dictionairy thatr links mac adress to rigidbody ID

    void Start()
    {
        DontDestroyOnLoad(gameObject);
        optitrackStreamingClient = GameObject.Find("OptitrackClient").GetComponent<OptitrackStreamingClient>(); //Find optitrack client

        //Fills dictionairy with possible mac adresses and corresponding rigidbody ID's
        for (int i = 0; i < blasterMacAdresses.Length; i++)
        {
            blasterMacAdressesAndRigidBodyIDs.Add(blasterRigidBodyIDs[i], blasterMacAdresses[i]);
        }

        InstantiateControllers();
    }

    //Checks which controllers need to be used
    public void InstantiateControllers()
    {
        switch (typeOfController)
        {
            case TypeOfController.None:
                break;
            case TypeOfController.ReverbControllers:
                InstantiateReverbControllers();
                break;
            case TypeOfController.Striker:
                InstantiateStriker();
                break;
            default:
                break;

        }
    }

    private void InstantiateReverbControllers()
    {
    }

    //Creates all blaster rigidbodies (also the ones that aren't used)
    private void InstantiateStriker()
    {

        OptitrackRigidBody newBlasterRigidBody;

        for (int i = 0; i < blasterMacAdresses.Length; i++)
        {
            newBlasterRigidBody = GameObject.Instantiate(blasterRigidBody);
            newBlasterRigidBody.transform.parent = this.transform;

            newBlasterRigidBody.RigidBodyId = blasterRigidBodyIDs[i];
            newBlasterRigidBody.StreamingClient = optitrackStreamingClient;
        }
    }

    //Instantiates 1 blaster controller for the input of the active weapon. It get's the rigidbody ID of the striker that in the collider and picks the right mac adress
    public void InstantiateBlasterController(int currentRigidBodyId)
    {
        BlasterOperator newBlasterController;

        newBlasterController = GameObject.Instantiate(blasterController);
        newBlasterController.transform.parent = this.transform;

        string currentStrikerMac;
        blasterMacAdressesAndRigidBodyIDs.TryGetValue(currentRigidBodyId, out currentStrikerMac);

        newBlasterController.StrikerMac = currentStrikerMac;

        newBlasterController.gameObject.SetActive(true);
    }
}
