using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondFloorDisabler : MonoBehaviour
{
    [SerializeField] LevelManager levelManager;
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<PlataformerPlayer>(out PlataformerPlayer player))
        {
            levelManager.DisableSecondFloor();
        }
    }
}
