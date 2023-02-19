using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlataformSwitch : MonoBehaviour
{
    [SerializeField] private ActivablePlataform plataform;

    private void OnTriggerStay(Collider other)
    {
        if (
            other.gameObject.TryGetComponent<CharacterController>(out CharacterController player)
            && Input.GetKeyDown(KeyCode.E)
        )
        {
            plataform.OnToggleActive();
        }
    }
}
