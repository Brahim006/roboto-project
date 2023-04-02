using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointCapsule : MonoBehaviour
{
    private GameManager gameManager;
    private CombatantPlayer player;

    private void Start()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(!other.isTrigger && other.TryGetComponent<CombatantPlayer>(out CombatantPlayer enteringPlayer))
        {
            player = enteringPlayer;
            gameManager.SetLastCheckpoint(player.transform.position);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Player") && player != null)
        {
            player.ReceiveHealing(1);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player") && player != null)
        {
            player = null;
        }
    }
}
