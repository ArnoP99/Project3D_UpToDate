using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectToKeep : MonoBehaviour
{
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }
}
