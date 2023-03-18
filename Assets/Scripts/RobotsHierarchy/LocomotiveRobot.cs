using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public abstract class LocomotiveRobot : RobotWithSounds
{
    private static readonly float FORWARD_SPEED = 3f;
    private static readonly float BACKWARDS_SPEED = 1.5f;
    private static readonly float ROTATION_SPEED = 90f;

    protected Animator animator;
    
    protected virtual void Start()
    {
        base.Start();
        animator = gameObject.GetComponentInChildren<Animator>();
    }

    protected void OnRobotMove(float verticalAxis, float horizontalAxis)
    {
        animator.SetFloat("vertical", verticalAxis);
        animator.SetFloat("horizontal", horizontalAxis);

        if (verticalAxis != 0)
        {
            transform.position += 
                transform.forward *
                verticalAxis *
                Time.deltaTime *
                (verticalAxis > 0 ? FORWARD_SPEED : BACKWARDS_SPEED);
        }

        if (horizontalAxis != 0)
        {
            transform.Rotate(Vector3.up, horizontalAxis * ROTATION_SPEED * Time.deltaTime);
        }
    }
}


