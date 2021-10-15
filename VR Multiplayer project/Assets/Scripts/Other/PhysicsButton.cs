using Mirror;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class PhysicsButton : MonoBehaviour
{
    [SerializeField] private float threshold = 0.1f;
    [SerializeField] private float deadZone = 0.025f;
    [SerializeField] public GameObject prefabAgressor;
    [SerializeField] public GameObject prefabNurse;

    private bool isPressed = false;
    private Vector3 startPos;
    private ConfigurableJoint joint;
    private Vector3 currentPos;

    public UnityEvent onPressed, onReleased;

    void Start()
    {
        startPos = transform.localPosition;
        joint = GetComponent<ConfigurableJoint>();
    }

    private void OnCollisionEnter(Collision collision)
    {

        Pressed();
        currentPos = collision.transform.parent.transform.parent.position;

        if (collision.gameObject.tag == "RightController" || collision.gameObject.tag == "LeftController")
        {
            GameObject visualRep = collision.gameObject.transform.parent.transform.parent.Find("VisualRepresentation").gameObject;
            GameObject player = collision.gameObject.transform.parent.transform.parent.transform.parent.gameObject;
            
            if (gameObject.tag == "AgressorButton")
            {
                player.SetActive(false);
                Destroy(visualRep.transform.gameObject.transform.GetChild(0).gameObject);                
                Instantiate(prefabAgressor, currentPos, Quaternion.identity, visualRep.transform);
                Debug.Log("pre");
                player.GetComponent<NetworkTransformChild>().target = visualRep.transform.gameObject.transform.GetChild(0);
                Debug.Log("post");
                player.SetActive(true);
            }
            if (gameObject.tag == "NurseButton")
            {
                player.SetActive(false);
                Destroy(visualRep.transform.gameObject.transform.GetChild(0).gameObject);
                Instantiate(prefabNurse, currentPos, Quaternion.identity, visualRep.transform);
                Debug.Log("pre");
                player.GetComponent<NetworkTransformChild>().target = visualRep.transform.gameObject.transform.GetChild(0);
                Debug.Log("post");
                player.SetActive(true);
            }
            if (gameObject.tag == "SceneButton")
            {
                SceneManager.LoadScene("ZiekenhuisKamer");
                Scene ziekenHuisKamer = SceneManager.GetSceneByName("ZiekenhuisKamer");
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        Released();
    }

    private float GetValue()
    {
        var value = Vector3.Distance(startPos, transform.localPosition) / joint.linearLimit.limit;

        if (Math.Abs(value) < deadZone)
        {
            value = 0;
        }
        return Mathf.Clamp(value, -1f, 1f);
    }

    private void Pressed()
    {
        isPressed = true;
        Debug.Log("Pressed");
        onPressed.Invoke();

    }

    private void Released()
    {
        isPressed = false;
        onReleased.Invoke();
        Debug.Log("Released");
    }
}
