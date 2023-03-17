using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class RobotWithSounds : RobotWithHealt
{
    protected AudioSource audioPlayer;
    [SerializeField] private AudioClip quickWalkSoundClip;
    [SerializeField] private AudioClip smoothWalkSoundClip;
    protected virtual void Start()
    {
        base.Start();
        audioPlayer = GetComponent<AudioSource>();
    }

    protected void PlayQuickWalkClip()
    {
        audioPlayer.clip = quickWalkSoundClip;
        audioPlayer.Play();
    }

    protected void PlaySmoothWalkClip()
    {
        audioPlayer.clip = smoothWalkSoundClip;
        audioPlayer.Play();
    }

    protected void StopAudio()
    {
        audioPlayer.Stop();
        audioPlayer.clip = null;
    }
}
