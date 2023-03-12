using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class LevelManager : MonoBehaviour
{
    private LevelManager instance;

    [SerializeField] Volume globalVolume;
    [SerializeField] GameObject secondFloor;
    [SerializeField] GameObject workerLegless;
    [SerializeField] GameObject guard;
    [SerializeField] GameObject stuckHead;

    [SerializeField] private Light thirdFloorLightA;
    [SerializeField] private Light thirdFloorLightB;

    private static readonly float LIGHT_TOGGLE_TIME = 4f;
    private static readonly float LIGHT_INTENSITY = 0.5f;
    private static readonly Vector3 GUARD_FINAL_POSITION = new Vector3(3, 2, 9.6f);
    private static readonly float ADD_TO_GUARD_FINAL_ROTATION = 30f;

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

    public void SetLevelFirstMilestone()
    {
        stuckHead.SetActive(true);
        ToggleGlobalVolumeStatus(false);
    }

    public void SetLevelSecondMilestone()
    {
        workerLegless.SetActive(true);
        stuckHead.SetActive(false);
        guard.transform.position = GUARD_FINAL_POSITION;
        guard.transform.Rotate(Vector3.up, ADD_TO_GUARD_FINAL_ROTATION);
        ToggleGlobalVolumeStatus(true);
    }

    public void OnTutorialLevelCompletion()
    {
        // TODO: Cambiar de escena y dar feedback al jugador
        Debug.Log("Terminado");
    }
    public bool IsLeglessWorkerActive()
    {
        return workerLegless.active;
    }

    public bool IsHeadStucked()
    {
        return stuckHead.active;
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

    private void ToggleGlobalVolumeStatus(bool original)
    {
        var l_volumeProfile = globalVolume.profile;
        if (l_volumeProfile.TryGet(out ColorAdjustments colorAdjustments))
        {
            colorAdjustments.colorFilter.overrideState = !original;
        }
        if (l_volumeProfile.TryGet(out Bloom bloom))
        {
            bloom.tint.overrideState = !original;
        }
    }
}
