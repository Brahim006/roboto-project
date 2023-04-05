using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialWorker : PlayerSpotter
{
    private WorkerState workerState = WorkerState.Idle;

    protected override void Start()
    {
        base.Start();
    }
    protected override void Update()
    {
        base.Update();
        OnSwitchAnimation();
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
        SpotPlayer();
    }

    public void StopRanting()
    {
        workerState = WorkerState.Idle;
        UnSpotlayer();
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
