using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class CombativeRobot : LocomotiveRobot
{
    private static readonly float COMBAT_MOVEMENT_SPEED = 2.5f;
    private static readonly int BLOCKING_DIVIDER = 2;
    private static readonly float NEXT_ATTACK_COOLDOWN = 1f;

    protected CombativeRobot target = null;
    private int _combatLayerIndex;
    private int lightAttackIndex = 0;
    private float _nextAttackOffset = NEXT_ATTACK_COOLDOWN;
    private bool _isAttacking;
    protected bool _isBlocking = false;
    protected bool _isMovementBlocked = false;
    protected virtual void Start()
    {
        base.Start();
        _combatLayerIndex = animator.GetLayerIndex("Combat");
    }

    protected virtual void Update()
    {
        base.Update();
        if (target != null)
        {
            CheckForAttackingState();
        }
    }

    public void SetTarget(CombativeRobot newTarget)
    {
        target = newTarget;
        if(animator.GetLayerWeight(_combatLayerIndex) != 1)
        {
            animator.SetLayerWeight(_combatLayerIndex, 1);
        }
    }

    public void UnTarget()
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
            Vector3 l_targetPosition = target.transform.position;
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

    protected void OnRobotAttack()
    {
        if (!_isAttacking)
        {
            _isAttacking = true;
            _isMovementBlocked = true;
            animator.SetBool("isAttacking", true);
        }
        lightAttackIndex++;
        if (lightAttackIndex > 4)
        {
            lightAttackIndex = 1;
        }
        _nextAttackOffset = NEXT_ATTACK_COOLDOWN;
        animator.SetInteger("lightAttacks", lightAttackIndex);
    }

    private void CheckForAttackingState()
    {
        if (_isAttacking)
        {
            _nextAttackOffset -= Time.deltaTime;
            if (_nextAttackOffset <= 0)
            {
                _isAttacking = false;
                _isMovementBlocked = false;
                lightAttackIndex = 0;
                animator.SetBool("isAttacking", false);
                animator.SetInteger("lightAttacks", lightAttackIndex);
            }
        }
    }

    protected void ToggleBlocking(bool block)
    {
        if(block)
        {
            _isBlocking = _isMovementBlocked = true;
            animator.SetBool("isBlocking", true);
        }
        else
        {
            _isBlocking = _isMovementBlocked = false;
            animator.SetBool("isBlocking", false);
        }
    }

    public void OnReceiveDamage(int amount)
    {
        if (_isBlocking)
        {
            base.OnReceiveDamage((int)Mathf.Floor(amount / BLOCKING_DIVIDER));
        }
        else
        {
            base.OnReceiveDamage(amount);
            animator.SetFloat("hitReaction", Random.Range(0, 2));
        }
        animator.SetTrigger("beingHit");
    }
}
