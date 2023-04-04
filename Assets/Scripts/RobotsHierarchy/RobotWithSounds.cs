using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class RobotWithSounds : RobotWithHealt
{
    protected AudioSource stepsAudioPlayer;
    protected virtual void Start()
    {
        base.Start();
        stepsAudioPlayer = GetComponents<AudioSource>()[0];
    }

    protected void PlayWalkClip()
    {
        if(!stepsAudioPlayer.isPlaying)
        {
            stepsAudioPlayer.Play();
        }
    }

    protected void StopWalkClip()
    {
        if (stepsAudioPlayer.isPlaying)
        {
            stepsAudioPlayer.Stop();
        }
    }
}
