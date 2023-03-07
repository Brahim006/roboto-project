using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Steps : MonoBehaviour
{
    private AudioSource audioSteps;
    void Start()
    {
        audioSteps = GetComponent<AudioSource>();
    }

    void Update()
    {
        
    }

    private void stopSteps()
    {

            audioSteps.Stop();
        
    }

    private void startSteps()
    {
        
            audioSteps.Play();
    }
}
