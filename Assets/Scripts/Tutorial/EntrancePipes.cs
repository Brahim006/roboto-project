using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntrancePipes : MonoBehaviour
{
    [SerializeField] private LevelManager levelManager;

    private void OnTriggerStay(Collider other)
    {
        if(
            Input.GetKeyDown(KeyCode.E) &&
            levelManager.IsHeadStucked() &&
            !levelManager.IsLeglessWorkerActive() &&
            other.gameObject.TryGetComponent<CharacterController>(out CharacterController player)
          )
        {
            player.PressButton(transform.position);
            levelManager.DeactivateStuckedHead();
            levelManager.ActivateLeglessWorker();
        }
    }
}
