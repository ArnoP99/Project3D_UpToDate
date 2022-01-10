using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandSanitizer : MonoBehaviour
{
    ParticleSystem handSanitizer;
    private void OnTriggerEnter(Collider other)
    {
        handSanitizer = GameObject.Find("Sanitizer_ParticleSystem").GetComponent<ParticleSystem>();

        handSanitizer.Play();
    }
}

