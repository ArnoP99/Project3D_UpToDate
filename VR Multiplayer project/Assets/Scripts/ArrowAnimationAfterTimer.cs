using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ArrowAnimationAfterTimer : MonoBehaviour
{
    Scene scene;
    float timeRemaining = 10;

    MeshRenderer pointerNurse;
    MeshRenderer pointerAgressor;

    bool isLoaded = true;
    bool sceneSet = true;

    void Update()
    {
        if (sceneSet)
        {
            scene = SceneManager.GetActiveScene();
            if (scene.name == "IntroductionRoom")
            {
                sceneSet = false;
            }
        }
        if (scene.name == "IntroductionRoom")
        {
            if (isLoaded)
            {
                pointerAgressor = GameObject.Find("ArrowAgressor").GetComponent<MeshRenderer>();
                pointerNurse = GameObject.Find("ArrowNurse").GetComponent<MeshRenderer>();
                pointerAgressor.enabled = false;
                pointerNurse.enabled = false;
                isLoaded = false;
                Debug.Log("turned off arrows");
            }
            timeRemaining -= Time.deltaTime;
            if (timeRemaining <= 0)
            {
                pointerAgressor.enabled = true;
                pointerNurse.enabled = true;
                Debug.Log("turned on arrows");
                scene = new Scene();
            }
        }


    }

}
