using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    
    [SerializeField] private CinemachineVirtualCamera intialCamera;
    [SerializeField] private CinemachineVirtualCamera alternativeCamera;
    public event Action<Vector3> OnChangeFoward;
    void Start()
    {
        OnChangeFoward?.Invoke(Vector3.forward);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            if(intialCamera.gameObject.active)
            {
                intialCamera.gameObject.SetActive(false);
                alternativeCamera.gameObject.SetActive(true);
                OnChangeFoward?.Invoke(Vector3.left);
            } else
            {
                intialCamera.gameObject.SetActive(true);
                alternativeCamera.gameObject.SetActive(false);
                OnChangeFoward?.Invoke(Vector3.forward);
            }
        }
    }
}
