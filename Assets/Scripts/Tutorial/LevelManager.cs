using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    private LevelManager instance;

    [SerializeField] GameObject secondFloor;
    [SerializeField] GameObject workerLegless;
    [SerializeField] GameObject stuckHead;

    [SerializeField] private Light thirdFloorLightA;
    [SerializeField] private Light thirdFloorLightB;

    private static readonly float LIGHT_TOGGLE_TIME = 4f;
    private static readonly float LIGHT_INTENSITY = 0.5f;

    private float _timeOffset;

    private void Awake()
    {
        if (instance is null)
        {
            instance = this;
        }
        else
        {
            Destroy(instance);
        }
    }
    void Start()
    {
        _timeOffset = LIGHT_TOGGLE_TIME;
        secondFloor.SetActive(false);
        thirdFloorLightA.intensity = LIGHT_INTENSITY;
        thirdFloorLightB.intensity = 0;
        workerLegless.SetActive(false);
        stuckHead.SetActive(false);
    }

    private void Update()
    {
        ToggleLight();
    }

    public void ActivateLeglessWorker()
    {
        workerLegless.SetActive(true);
    }

    public void ActivateStuckedHead()
    {
        stuckHead.SetActive(true);
    }
    private void ToggleLight()
    {
        _timeOffset -= Time.deltaTime;
        if(_timeOffset <= 0)
        {
            _timeOffset = LIGHT_INTENSITY;
            if(thirdFloorLightA.intensity == 0)
            {
                thirdFloorLightA.intensity = LIGHT_INTENSITY;
                thirdFloorLightB.intensity = 0;
            } else
            {
                thirdFloorLightA.intensity = 0;
                thirdFloorLightB.intensity = LIGHT_INTENSITY;
            }
        }
    }

    public void ActivateSecondFloor()
    {
        if(!secondFloor.active)
        {
            secondFloor.SetActive(true);
        }
    }

    public void DisableSecondFloor()
    {
        if(secondFloor.active)
        {
            secondFloor.SetActive(false);
        }
    }
}
