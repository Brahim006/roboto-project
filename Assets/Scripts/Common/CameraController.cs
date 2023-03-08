using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera intialCamera;
    [SerializeField] private CinemachineVirtualCamera alternativeCamera;

    public event Action<Vector3> OnForwardChange;

    void Start()
    {
        OnForwardChange?.Invoke(Vector3.forward);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            if(intialCamera.gameObject.active)
            {
                intialCamera.gameObject.SetActive(false);
                alternativeCamera.gameObject.SetActive(true);
                OnForwardChange?.Invoke(Vector3.left);
            } else
            {
                intialCamera.gameObject.SetActive(true);
                alternativeCamera.gameObject.SetActive(false);
                OnForwardChange?.Invoke(Vector3.forward);
            }
        }
    }
}
