using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterFactoryCinematics : MonoBehaviour
{
    [SerializeField] private GameObject layingRobot;
    [SerializeField] private GameObject combatantModel;
    [SerializeField] private RoboticWelder roboticWelder;
    [SerializeField] private CombativeGuard enemyGuard;
    [SerializeField] private CombatantPlayer player;
    [SerializeField] private GameObject HUD;

    [SerializeField] private CinemachineVirtualCamera frontCamera;
    [SerializeField] private CinemachineVirtualCamera angularCamera;
    [SerializeField] private CinemachineVirtualCamera initialCamera;

    private Animator layingRobotAnimator;
    void Start()
    {
        layingRobotAnimator = layingRobot.GetComponent<Animator>();
    }

    public void ActivateLayingRobot()
    {
        layingRobot.SetActive(true);
        roboticWelder.PlayAnimation();
    }

    public void MakeRobotSuffer()
    {
        if(!layingRobotAnimator.GetBool("isBeingWelded"))
        {
            layingRobotAnimator.SetBool("isBeingWelded", true);
        }
    }

    public void EndRobotsPain()
    {
        if (layingRobotAnimator.GetBool("isBeingWelded"))
        {
            layingRobotAnimator.SetBool("isBeingWelded", false);
        }
    }

    public void SpawnCombatantModel()
    {
        Destroy(layingRobot);
        combatantModel.SetActive(true);
        enemyGuard.gameObject.SetActive(true);
    }

    public void TogleLowerCamera(bool front)
    {
        if(front)
        {
            angularCamera.gameObject.SetActive(true);
            frontCamera.gameObject.SetActive(false);
        }
        else
        {
            frontCamera.gameObject.SetActive(true);
            angularCamera.gameObject.SetActive(false);
        }
    }

    public void ActivateHUD()
    {
        HUD.SetActive(true);
    }

    public void EndCinematic()
    {
        frontCamera.gameObject.SetActive(false);
        initialCamera.gameObject.SetActive(true);
        enemyGuard.gameObject.SetActive(true);
        player.gameObject.SetActive(true);
        GameObject.FindObjectOfType<GameManager>().SetLastCheckpoint(player.transform.position);
        Destroy(gameObject);
    }
}
