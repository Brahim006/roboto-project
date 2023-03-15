using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class RobotWithSounds : RobotWithHealt
{
    protected AudioSource audioPlayer;
    [SerializeField] private AudioClip walkingSoundClip;
    [SerializeField] private AudioClip runningSoundClip;
    protected virtual void Start()
    {
        base.Start();
        audioPlayer = GetComponent<AudioSource>();
    }

    protected void PlayWalkingClip()
    {
        audioPlayer.clip = walkingSoundClip;
        audioPlayer.Play();
    }

    protected void PlayRunningClip()
    {
        audioPlayer.clip = runningSoundClip;
        audioPlayer.Play();
    }

    protected void StopAudio()
    {
        audioPlayer.Stop();
        audioPlayer.clip = null;
    }
}
