using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlataformSwitch : MonoBehaviour
{
    [SerializeField] private ActivablePlataform plataform;

    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKeyDown(KeyCode.E) && other.TryGetComponent<PlataformerPlayer>(out PlataformerPlayer player))
        {
            plataform.OnToggleActive();
            player.PressButton(transform.position);
        }
    }
}
