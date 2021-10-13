using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PhysicsButton : MonoBehaviour
{
    [SerializeField] private float threshold = 0.1f;
    [SerializeField] private float deadZone = 0.025f;
    [SerializeField] public GameObject prefabAgressor;
    [SerializeField] public GameObject prefabNurse;

    private bool isPressed = false;
    private Vector3 startPos;
    private ConfigurableJoint joint;

    public UnityEvent onPressed, onReleased;

    void Start()
    {
        startPos = transform.localPosition;
        joint = GetComponent<ConfigurableJoint>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        Pressed();
    }

    private void OnCollisionExit(Collision collision)
    {
        Released();

        if (collision.gameObject.tag == "RightController" || collision.gameObject.tag == "LeftController")
        {
            GameObject visualRep = collision.gameObject.transform.parent.transform.parent.Find("VisualRepresentation").gameObject;

            if (gameObject.tag == "AgressorButton")
            {
                Destroy(visualRep.transform.gameObject.transform.GetChild(0).gameObject);
                Instantiate(prefabAgressor, Vector3.zero, Quaternion.identity, visualRep.transform);

            }
            if (gameObject.tag == "NurseButton")
            {
                Destroy(visualRep.transform.gameObject.transform.GetChild(0).gameObject);
                Instantiate(prefabNurse, Vector3.zero, Quaternion.identity, visualRep.transform);
            }
        }
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
