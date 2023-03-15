using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public abstract class LocomotiveRobot : RobotWithSounds
{
    private static readonly float WALK_SPEED = 2f;
    private static readonly float RUN_SPEED = 4f;

    private Animator animator;

    protected AnimationState animationState = AnimationState.Idle;
    
    protected virtual void Start()
    {
        base.Start();
        animator = gameObject.GetComponentInChildren<Animator>();
    }

    protected virtual void Update()
    {
        OnAnimationSwitch();
    }

    protected void OnRobotWalk(Vector3 direction, bool running)
    {
        float speed;
        if (running)
        {
            speed = RUN_SPEED;
            animationState = AnimationState.Running;
        }
        else
        {
            speed = WALK_SPEED;
            animationState = AnimationState.Walking;
        }
        transform.position += direction * speed * Time.deltaTime;
    }

    protected enum AnimationState
    {
        Idle,
        Walking,
        Running,
        Jumping,
        Falling,
        Landing,
        PressingButton
    }

    private void OnAnimationSwitch()
    {
        switch (animationState)
        {
            case AnimationState.Walking:
                if (animator.GetInteger("animation") != 1) animator.SetInteger("animation", 1);
                break;
            case AnimationState.Running:
                if (animator.GetInteger("animation") != 2) animator.SetInteger("animation", 2);
                break;
            case AnimationState.Jumping:
                if (animator.GetInteger("animation") != 3) animator.SetInteger("animation", 3);
                break;
            case AnimationState.Falling:
                if (animator.GetInteger("animation") != 4) animator.SetInteger("animation", 4);
                break;
            case AnimationState.Landing:
                if (animator.GetInteger("animation") != 5) animator.SetInteger("animation", 5);
                break;
            case AnimationState.PressingButton:
                if (animator.GetInteger("animation") != 6) animator.SetInteger("animation", 6);
                break;
            default:
                if (animator.GetInteger("animation") != 0) animator.SetInteger("animation", 0);
                break;
        }
    }
}


