using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ArrowAnimationAfterTimer : MonoBehaviour
{
    Scene scene;
    float timeRemaining = 5;

    MeshRenderer pointerNurse;
    MeshRenderer pointerAgressor;

    bool isLoaded = true;
    // Start is called before the first frame update
    void Start()
    {


        // Update is called once per frame
        void Update()
        {
            if (scene.name == "IntroductionRoom")
            {
                if (isLoaded)
                {
                    pointerAgressor = GameObject.Find("ArrowAgressor").gameObject.GetComponent<MeshRenderer>();
                    pointerNurse = GameObject.Find("ArrowNurse").gameObject.GetComponent<MeshRenderer>();
                    pointerAgressor.enabled = false;
                    pointerNurse.enabled = false;
                    isLoaded = false;
                }
                timeRemaining -= Time.deltaTime;
                if (timeRemaining <= 0)
                {
                    pointerAgressor.enabled = true;
                    pointerNurse.enabled = true;
                }
            }


        }
    }
}
