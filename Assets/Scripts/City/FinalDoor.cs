using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalDoor : MonoBehaviour
{
    private static readonly float MINIMUM_HEIGHT = -6.3f;

    [SerializeField] private CityLevelManager levelManager;
    [SerializeField] private CombativeGuard[] guardsToKill;
    private GameManager gameManager;
    private BoxCollider animationTrigger;
    private AudioSource audioSource;

    private int _guardsLeft;
    private bool _isActive = false;
    void Start()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>();
        audioSource = GetComponent<AudioSource>();
        if(gameManager.cityLevelState == 1)
        {
            animationTrigger = gameObject.GetComponent<BoxCollider>();
            _guardsLeft = guardsToKill.Length;
            foreach (CombativeGuard guard in guardsToKill)
            {
                guard.OnDeath += OnKillGuard;
            }
        }
    }

    void Update()
    {
        if(_isActive)
        {
            if (transform.position.y >= MINIMUM_HEIGHT)
            {
                transform.position += Vector3.down * Time.deltaTime;
            }
            else
            {
                animationTrigger.enabled = true;
                _isActive = false;
                audioSource.Stop();
            }
        }
    }

    private void OnKillGuard()
    {
        _guardsLeft--;
        if(_guardsLeft == 0)
        {
            _isActive = true;
            audioSource.Play();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.isTrigger && other.CompareTag("Player"))
        {
            gameManager.OnGameQuit();
        }
    }
}
