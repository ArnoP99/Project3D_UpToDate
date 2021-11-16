using UnityEngine;
using TMPro;

public class TriggerEvents : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "LeftController" || collision.gameObject.tag == "RightController")
        {
            gameObject.GetComponent<TextMeshPro>().color = Color.red;

        }
    }

    private void OnCollisionStay(Collision collision)
    {
        gameObject.tag = "ActiveChoice";
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "LeftController" || collision.gameObject.tag == "RightController")
        {
            gameObject.GetComponent<TextMeshPro>().color = Color.white;
            gameObject.tag = "InactiveChoice";
        }
    }
}
