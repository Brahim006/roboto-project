using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadLightController : MonoBehaviour
{
    private void Start()
    {
        gameObject.SetActive(false);
    }

    void Update()
    {
        OnToggleLight();
    }
    private void OnToggleLight()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            gameObject.SetActive(!gameObject.active);
        }
    }
}
