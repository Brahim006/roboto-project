using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorsSwitch : MonoBehaviour
{
    [SerializeField] private Elevator elevatorA;
    [SerializeField] private Elevator elevatorB;
    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKeyDown(KeyCode.E) && other.TryGetComponent<PlataformerPlayer>(out PlataformerPlayer player))
        {
            elevatorA.ToggleActive();
            elevatorB.ToggleActive();
            player.PressButton(transform.position);
        }
    }
}
