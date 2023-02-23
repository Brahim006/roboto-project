using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlataformSwitch : MonoBehaviour
{
    [SerializeField] private ActivablePlataform plataform;

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == 6 && Input.GetKeyDown(KeyCode.E))
        {
            plataform.OnToggleActive();
            other.TryGetComponent<CharacterController>(out CharacterController controller);
            if(controller != null) controller.PressButton(transform.position);
        }
    }
}
