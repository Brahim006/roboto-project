using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSystem : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera initialCamera;

    private CinemachineVirtualCamera _activeCamera;
    void Start()
    {
        _activeCamera = initialCamera;
    }

    public void OnChangeCamera(CinemachineVirtualCamera enteringCamera)
    {
        if(!enteringCamera.gameObject.active)
        {
            enteringCamera.gameObject.SetActive(true);
            _activeCamera.gameObject.SetActive(false);
            _activeCamera = enteringCamera;
        }
    }
}
