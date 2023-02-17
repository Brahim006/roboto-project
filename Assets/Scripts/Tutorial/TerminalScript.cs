using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerminalScript : MonoBehaviour
{
    [SerializeField] LevelManager levelManager;
    private float _timeFromLastInstanciation = 0;

    private void Update()
    {
        _timeFromLastInstanciation += Time.deltaTime;
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.TryGetComponent<CharacterController>(out CharacterController player))
        {
            if(Input.GetKeyDown(KeyCode.E) && _timeFromLastInstanciation > 1)
            {
                levelManager.InstanciateRobotParts();
                _timeFromLastInstanciation = 0;
            }
        }
    }

}
