using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Steps : MonoBehaviour
{
    private AudioSource audioPlayer;
    [SerializeField] private AudioClip walking;
    [SerializeField] private AudioClip running;
    [SerializeField] private CharacterController player;
    void Start()
    {
        audioPlayer = GetComponent<AudioSource>();
        player.OnStartWalking.AddListener(StartWalking);
        player.OnStartRunning.AddListener(StartRunning);
        player.OnStopMoving.AddListener(StopAudio);
    }

    void Update()
    {
        
    }
    public void StartWalking(bool audio)
    {
        Debug.Log($"OnStartWalking recibido por {gameObject.name}");
        audioPlayer.clip = walking;
        audioPlayer.Play();
    }

    public void StartRunning(bool audio)
    {
        Debug.Log($"OnStartRunning recibido por {gameObject.name}");
        audioPlayer.clip = running;
        audioPlayer.Play();
    }

    public void StopAudio(bool audio)
    {
        Debug.Log($"OnStopMoving recibido por {gameObject.name}");
        audioPlayer.Stop();
        audioPlayer.clip = null;
    }
}
