using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceChamber : MonoBehaviour
{
    [SerializeField] private EnterFactoryCinematics cinematicManager;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "FallingWorker")
        {
            Destroy(other.gameObject);
            cinematicManager.ActivateLayingRobot();
            cinematicManager.TogleLowerCamera(true);
        }
    }
}
