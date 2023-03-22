using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class CombativeRobot : LocomotiveRobot
{
    private static readonly float COMBAT_MOVEMENT_SPEED = 2.5f;

    protected Transform target = null;
    private int _combatLayerIndex;
    protected virtual void Start()
    {
        base.Start();
        _combatLayerIndex = animator.GetLayerIndex("Combat");
    }

    protected virtual void Update()
    {
        base.Update();
    }

    protected void SetTarget(Transform newTarget)
    {
        target = newTarget;
        if(animator.GetLayerWeight(_combatLayerIndex) != 1)
        {
            animator.SetLayerWeight(_combatLayerIndex, 1);
        }
    }

    protected void UnTarget()
    {
        target = null;
        StopLookingAt();
        animator.SetLayerWeight(_combatLayerIndex, 0);
    }

    protected virtual void OnRobotMove(float verticalAxis, float horizontalAxis)
    {
        if(target is null)
        {
            base.OnRobotMove(verticalAxis, horizontalAxis);
        }
        else
        {
            animator.SetFloat("vertical", verticalAxis);
            animator.SetFloat("horizontal", horizontalAxis);
            Vector3 l_targetPosition = target.position;
            l_targetPosition.y = transform.position.y;
            LookAtTarget(l_targetPosition);
            if (verticalAxis != 0)
            {
                transform.position += transform.forward * verticalAxis * COMBAT_MOVEMENT_SPEED * Time.deltaTime;
            }
            if(horizontalAxis != 0)
            {
                transform.position += transform.right * horizontalAxis * COMBAT_MOVEMENT_SPEED * Time.deltaTime;
            }
        }
    }
}
