using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CombatantPlayer : CombativeRobot
{
    private static readonly float JUMP_MAGNITUDE = 5f;
    private static readonly float FALLING_VELOCITY_THRESHOLD = -1f;

    private Rigidbody rigidbody;
    private List<CombativeGuard> enemiesNearby;

    private bool _isFalling = false;
    protected override void Start()
    {
        base.Start();
        rigidbody = GetComponent<Rigidbody>();
        enemiesNearby = new List<CombativeGuard>();
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
            // Fighting exclusive
            if(target != null)
            {
                OnChangeTarget();
                OnPlayerAttack();
                checkForBlockingState();
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
            OnRobotAttack();
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
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            ToggleBlocking(true);
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            ToggleBlocking(false);
        }
    }

    private void OnChangeTarget()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            ReTarget();
        }
    }

    private void ReTarget()
    {
        var newTarget = enemiesNearby.Find(enemy => enemy != target);
        SetTarget(newTarget);
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Enemy"))
        {
            var enemy = other.GetComponent<CombativeGuard>();
            if(target is null)
            {
                SetTarget(enemy);
            }
            enemiesNearby.Add(enemy);
            //enemy.SetTarget(this);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            var enemy = other.GetComponent<CombativeGuard>();
            enemiesNearby.Remove(enemy);
            if (enemiesNearby.Count == 0)
            {
                UnTarget();
            }
            else if(enemy == target)
            {
                ReTarget();
            }
        }
    }
}
