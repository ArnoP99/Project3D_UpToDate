using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowAnimationAfterTimer : MonoBehaviour
{

    float timeRemaining = 5;

    MeshRenderer pointerNurse;
    MeshRenderer pointerAgressor;
    // Start is called before the first frame update
    void Start()
    {
        pointerAgressor = GameObject.FindGameObjectWithTag("PointerAgressor").GetComponent<MeshRenderer>();
        pointerNurse = GameObject.FindGameObjectWithTag("PointerNurse").GetComponent<MeshRenderer>();
        pointerAgressor.enabled = false;
        pointerNurse.enabled = false;

        // Update is called once per frame
        void Update()
        {

            timeRemaining -= Time.deltaTime;
            if (timeRemaining <= 0)
            {
                pointerAgressor.enabled = true;
                pointerNurse.enabled = true;
            }

        }
    }
}
