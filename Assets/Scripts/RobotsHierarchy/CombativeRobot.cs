using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class CombativeRobot : LocomotiveRobot
{
    private static readonly float COMBAT_MOVEMENT_SPEED = 2.5f;
    private static readonly int BLOCKING_DIVIDER = 2;
    private static readonly float NEXT_ATTACK_COOLDOWN = 1f;
    protected static readonly float HIT_DISTANCE = 1.7f;
    protected static readonly float PARTICLE_EMISION_TIME = 1f;

    protected CombativeRobot target = null;
    private GameObject sparkEmitter;
    private GameObject nutsAndBoltsEmitter;

    protected int _combatLayerIndex;
    protected int lightAttackIndex = 0;
    private float _sparksEmitterOffset = PARTICLE_EMISION_TIME;
    private float _nutsAndBoltsEmitterOffset = PARTICLE_EMISION_TIME;
    private float _nextAttackOffset = NEXT_ATTACK_COOLDOWN;
    private bool _isAttacking;
    protected bool _isBlocking = false;
    protected bool _isMovementBlocked = false;
    protected virtual void Start()
    {
        base.Start();
        sparkEmitter = transform.GetChild(1).gameObject;
        sparkEmitter.SetActive(false);
        nutsAndBoltsEmitter = transform.GetChild(2).gameObject;
        nutsAndBoltsEmitter.SetActive(false);
        _combatLayerIndex = animator.GetLayerIndex("Combat");
    }

    protected virtual void Update()
    {
        base.Update();
        if (target != null)
        {
            CheckForAttackingState();
        }
        CheckForSparksEmission();
        CheckForNutsAndBoltsEmission();
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
                PlayWalkClip();
            }
            if(horizontalAxis != 0)
            {
                transform.position += transform.right * horizontalAxis * COMBAT_MOVEMENT_SPEED * Time.deltaTime;
                PlayWalkClip();
            }

            if (verticalAxis == 0 && horizontalAxis == 0)
            {
                StopWalkClip();
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
        _isBlocking = _isMovementBlocked = block;
        animator.SetBool("isBlocking", block);
    }

    private void CheckForSparksEmission()
    {
        if(sparkEmitter.active)
        {
            _sparksEmitterOffset -= Time.deltaTime;
            if(_sparksEmitterOffset <= 0)
            {
                sparkEmitter.SetActive(false);
            }
        }
    }

    private void CheckForNutsAndBoltsEmission()
    {
        if (nutsAndBoltsEmitter.active)
        {
            _nutsAndBoltsEmitterOffset -= Time.deltaTime;
            if (_nutsAndBoltsEmitterOffset <= 0)
            {
                nutsAndBoltsEmitter.SetActive(false);
            }
        }
    }

    public void OnReceiveDamage(int amount)
    {
        if (_isBlocking)
        {
            base.OnReceiveDamage((int)Mathf.Floor(amount / BLOCKING_DIVIDER));
            sparkEmitter.SetActive(true);
            _sparksEmitterOffset = PARTICLE_EMISION_TIME;
        }
        else
        {
            base.OnReceiveDamage(amount);
            animator.SetFloat("hitReaction", Random.Range(0, 2));
            nutsAndBoltsEmitter.SetActive(true);
            _nutsAndBoltsEmitterOffset = PARTICLE_EMISION_TIME;
        }
        animator.SetTrigger("beingHit");
    }
}
