using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwapper : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera enterCamera;
    [SerializeField] private CameraSystem cameraSystem;

    private void OnTriggerEnter(Collider other)
    {
        if(!other.isTrigger && other.CompareTag("Player"))
        {
            cameraSystem.OnChangeCamera(enterCamera);
        }
    }
}
