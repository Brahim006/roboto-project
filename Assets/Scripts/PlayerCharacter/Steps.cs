using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Steps : MonoBehaviour
{
    private AudioSource audioPlayer;
    [SerializeField]private AudioClip walking;
    [SerializeField]private AudioClip running;
    [SerializeField] private CharacterController character;
    void Start()
    {
        audioPlayer = GetComponent<AudioSource>();
        character.StartToWalking.AddListener(StartWalking);
        character.StartToRunnig.AddListener(StartRunning);
        character.StopMoving.AddListener(StopAudio);
    }

    void Update()
    {
        
    }
    public void StartWalking(bool audio)
    {
        audioPlayer.clip = walking;
        audioPlayer.Play();
        Debug.Log($"StartToWalking recibido por {gameObject.name}");
    }

    public void StartRunning(bool audio)
    {
        audioPlayer.clip = running;
        audioPlayer.Play();
        Debug.Log($"StartToRunning recibido por {gameObject.name}");
    }

    public void StopAudio(bool audio)
    {
        audioPlayer.Stop();
        audioPlayer.clip = null;
        Debug.Log($"StopMoving recibido por {gameObject.name}");
    }

}
