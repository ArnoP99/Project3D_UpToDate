using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class TriggerEvents : MonoBehaviour
{

    // Start is called before the first frame update
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "LeftController" || collision.gameObject.tag == "RightController")
        {
            
            gameObject.GetComponent<TextMeshPro>().color = Color.red;
        }
        Debug.Log("Tag: " + collision.gameObject.tag);
        Debug.Log("TriggerEnter");
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "LeftController" || collision.gameObject.tag == "RightController")
        {
            gameObject.GetComponent<TextMeshPro>().color = Color.white;
        }
        Debug.Log("Tag: " + collision.gameObject.tag);
        Debug.Log("TriggerExit");
    }
}
