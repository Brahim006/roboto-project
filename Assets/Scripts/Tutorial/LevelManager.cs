using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    private LevelManager instance;

    [SerializeField] GameObject secondFloor;
    [SerializeField] GameObject thirdFloor;

    [SerializeField] Transform piecesInstanciatorMembers;
    [SerializeField] Transform piecesInstanciatorCore;
    [SerializeField] GameObject WorkerBody;
    [SerializeField] GameObject WorkerHead;
    [SerializeField] GameObject WorkerArm;
    [SerializeField] GameObject WorkerLeg;

    [SerializeField] private Light thirdFloorLightA;
    [SerializeField] private Light thirdFloorLightB;

    private static readonly float LIGHT_TOGGLE_TIME = 4f;
    private static readonly float LIGHT_INTENSITY = 0.5f;

    private float _timeOffset;
    void Start()
    {
        if(instance is null)
        {
            instance = this;
            _timeOffset = LIGHT_TOGGLE_TIME;
            secondFloor.SetActive(false);
            thirdFloorLightA.intensity = LIGHT_INTENSITY;
            thirdFloorLightB.intensity = 0;
        }
        else
        {
            Destroy(instance);
        }
    }

    private void Update()
    {
        ToggleLight();
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
