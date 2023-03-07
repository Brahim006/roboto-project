using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Steps : MonoBehaviour
{
    private AudioSource audioPlayer;
    private AudioClip walking;
    private AudioClip running;
    void Start()
    {
        audioPlayer = GetComponent<AudioSource>();
    }

    void Update()
    {
        
    }
    public void StartWalking()
    {
        audioPlayer.clip = walking;
        audioPlayer.Play();
    }

    public void StartRunning()
    {
        audioPlayer.clip = running;
        audioPlayer.Play();
    }

    public void StopAudio()
    {
        audioPlayer.Stop();
        audioPlayer.clip = null;
    }

}
