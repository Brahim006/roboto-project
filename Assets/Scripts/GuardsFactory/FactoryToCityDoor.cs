using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FactoryToCityDoor : MonoBehaviour
{
    private static readonly float MINIMUM_HEIGHT = -2.5f;

    [SerializeField] private FactoryLevelManager levelManager;
    [SerializeField] private CombativeGuard[] guardsToKill;
    private AudioSource audioSource;

    private int _guardsLeft;
    void Start()
    {
        _guardsLeft = guardsToKill.Length;
        audioSource = GetComponent<AudioSource>();
        foreach (CombativeGuard guard in guardsToKill)
        {
            guard.OnDeath += OnKillGuard;
        }
    }
    void Update()
    {
        if(_guardsLeft == 0 && transform.position.y >= MINIMUM_HEIGHT)
        {
            transform.position += Vector3.down * Time.deltaTime;
        }
    }

    private void OnKillGuard()
    {
        _guardsLeft--;
        audioSource.Play();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(!other.isTrigger && other.CompareTag("Player"))
        {
            levelManager.WinCondition(other.gameObject.GetComponent<CombatantPlayer>().GetHealth());
        }
    }
}
