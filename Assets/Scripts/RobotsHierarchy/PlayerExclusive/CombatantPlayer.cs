using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CombatantPlayer : CombativeRobot
{
    private static readonly float JUMP_MAGNITUDE = 5f;
    private static readonly float FALLING_VELOCITY_THRESHOLD = -1f;
    private static readonly int BLOCKING_DIVIDER = 2;
    private static readonly float NEXT_ATTACK_COOLDOWN = 1f;

    [SerializeField] private Transform testTarget;

    private Rigidbody rigidbody;

    private int lightAttackIndex = 0;
    private float _nextAttackOffset = NEXT_ATTACK_COOLDOWN;
    private bool _isFalling = false;
    private bool _isBlocking = false;
    private bool _isAttacking;
    private bool _isMovementBlocked = false;
    protected override void Start()
    {
        base.Start();
        rigidbody = GetComponent<Rigidbody>();
    }

    protected override void Update()
    {
        base.Update();
        CheckForFallingState();
        if (!_isFalling)
        {
            if(!_isMovementBlocked)
            {
                OnPlayerWalk();
                OnPlayerJump();
            }
            if(Input.GetKeyDown(KeyCode.Tab))
            {
                if(target is null)
                {
                    SetTarget(testTarget);
                }
                else
                {
                    UnTarget();
                }
            }
            // Fighting exclusive
            if(target != null)
            {
                OnPlayerAttack();
                checkForBlockingState();
                CheckForAttackingState();
            }
        }
    }

    private void OnPlayerWalk()
    {
        var l_vertical = Input.GetAxisRaw("Vertical");
        if (target is null && l_vertical != 0)
        {
            OnRobotMove(
            Input.GetAxisRaw("Vertical"),
            Input.GetAxis("Horizontal")
            );
        }
        else
        {
            OnRobotMove(
            Input.GetAxisRaw("Vertical"),
            Input.GetAxisRaw("Horizontal")
            );
        }

    }

    private void OnPlayerJump()
    {
        if (!_isFalling && Input.GetKeyDown(KeyCode.Space))
        {
            if(target is null)
            {
                var l_vertical = Input.GetAxisRaw("Vertical");
                Vector3 l_jumpDirection = (transform.forward * l_vertical) + Vector3.up;
                rigidbody.AddForceAtPosition(l_jumpDirection * JUMP_MAGNITUDE, transform.position, ForceMode.Impulse);
                animator.SetTrigger("jumps");
            }
        }
    }

    private void OnPlayerAttack()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if(!_isAttacking)
            {
                _isAttacking = true;
                _isMovementBlocked = true;
                animator.SetBool("isAttacking", true);
            }
            lightAttackIndex++;
            if(lightAttackIndex > 4)
            {
                lightAttackIndex = 1;
            }
            _nextAttackOffset = NEXT_ATTACK_COOLDOWN;
            animator.SetInteger("lightAttacks", lightAttackIndex);
        }
    }

    private void CheckForAttackingState()
    {
        if(_isAttacking)
        {
            _nextAttackOffset -= Time.deltaTime;
            if(_nextAttackOffset <= 0)
            {
                _isAttacking = false;
                _isMovementBlocked = false;
                lightAttackIndex = 0;
                animator.SetBool("isAttacking", false);
                animator.SetInteger("lightAttacks", lightAttackIndex);
            }
        }
    }

    private void CheckForFallingState()
    {
        if (rigidbody.velocity.y < FALLING_VELOCITY_THRESHOLD)
        {
            // Está cayendo
            _isFalling = true;
            animator.SetBool("isFalling", true);
        }
        else
        {
            // No está cayendo
            _isFalling = false;
            animator.SetBool("isFalling", false);
        }
    }

    private void checkForBlockingState()
    {
        if(Input.GetKeyDown(KeyCode.LeftShift))
        {
            _isBlocking = _isMovementBlocked = true;
            animator.SetBool("isBlocking", true);
        }
        if(Input.GetKeyUp(KeyCode.LeftShift))
        {
            _isBlocking = _isMovementBlocked = false;
            animator.SetBool("isBlocking", false);
        }
    }

    public void OnReceiveDamage(int amount)
    {
        animator.SetTrigger("beingHit");
        if(_isBlocking)
        {
            base.OnReceiveDamage((int) Mathf.Floor(amount / BLOCKING_DIVIDER));
        }
        else
        {
            base.OnReceiveDamage(amount);
        }
        // TODO: Implementar animaciones de golpes
    }
}
