using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera intialCamera;
    [SerializeField] private CinemachineVirtualCamera alternativeCamera;
    void Start()
    {
        
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
            } else
            {
                intialCamera.gameObject.SetActive(true);
                alternativeCamera.gameObject.SetActive(false);
            }
        }
    }
}
