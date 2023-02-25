using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadLightController : MonoBehaviour
{
    private Light light;
    private void Start()
    {
        light = GetComponent<Light>();
        light.enabled = false;
    }

    void Update()
    {
        OnToggleLight();
    }
    private void OnToggleLight()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if(light.isActiveAndEnabled)
            {
                light.enabled = false;
            }
            else
            {
                light.enabled = true;
            }
        }
    }
}
