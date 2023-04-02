using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    private LevelManager instance;

    private GameManager gameManager;
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
        gameManager = GameObject.FindObjectOfType<GameManager>();
        _timeOffset = LIGHT_TOGGLE_TIME;
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
    }

    public void SetLevelSecondMilestone()
    {
        workerLegless.SetActive(true);
        stuckHead.SetActive(false);
        guard.transform.position = GUARD_FINAL_POSITION;
        guard.transform.Rotate(Vector3.up, ADD_TO_GUARD_FINAL_ROTATION);
    }

    public void OnTutorialLevelCompletion()
    {
        gameManager.TransitionFromTutorialToCity();
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
}
