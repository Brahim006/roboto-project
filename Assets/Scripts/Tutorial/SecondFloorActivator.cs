using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondFloorActivator : MonoBehaviour
{
    private CharacterController player;
    private LevelController levelController;
    void Start()
    {
        levelController = LevelController.GetInstance();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.TryGetComponent<CharacterController>(out player))
        {
            Debug.Log(levelController);
            levelController.SwitchSecondFloor();
        }
    }
}
