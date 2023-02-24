using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialWorker : MonoBehaviour
{
    [SerializeField] private CharacterController player;
    private Animator animator;
    private WorkerState workerState = WorkerState.Idle;

    private bool _isRanting = false;
    private Vector3 _originalForward;
    void Start()
    {
        animator = GetComponent<Animator>();
        _originalForward = transform.forward;
    }

    void Update()
    {
        OnSwitchAnimation();
        if(_isRanting)
        {
            transform.LookAt(player.transform.position);
        }
        else
        {
            transform.LookAt(_originalForward);
        }
    }

    public void Rant()
    {
        var animation = Random.Range(0, 2);
        switch(animation)
        {
            case 0:
                workerState = WorkerState.AnoyedHeadShake;
                break;
            case 1:
                workerState = WorkerState.ShakeFist;
                break;
            case 2:
                workerState = WorkerState.ShakingHeadNo;
                break;
        }
        _isRanting = true;
    }

    public void StopRanting()
    {
        workerState = WorkerState.Idle;
        _isRanting = false;
    }

    private void OnSwitchAnimation()
    {
        switch(workerState)
        {
            case WorkerState.AnoyedHeadShake:
                if (animator.GetInteger("animation") != 1) animator.SetInteger("animation", 1);
                break;
            case WorkerState.ShakeFist:
                if (animator.GetInteger("animation") != 2) animator.SetInteger("animation", 2);
                break;
            case WorkerState.ShakingHeadNo:
                if (animator.GetInteger("animation") != 3) animator.SetInteger("animation", 3);
                break;
            default:
                if (animator.GetInteger("animation") != 0) animator.SetInteger("animation", 0);
                break;
        }
    }
}

enum WorkerState {
    Idle,
    AnoyedHeadShake,
    ShakeFist,
    ShakingHeadNo
}
