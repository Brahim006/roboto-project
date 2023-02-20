using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondFloorDisabler : MonoBehaviour
{
    [SerializeField] LevelManager levelManager;
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<CharacterController>(out CharacterController player))
        {
            levelManager.DisableSecondFloor();
        }
    }
}
