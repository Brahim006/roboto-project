using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TunnelToFactory : MonoBehaviour
{
    [SerializeField] private CityLevelManager levelManager;
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            levelManager.EnterFactory();
        }
    }
}
