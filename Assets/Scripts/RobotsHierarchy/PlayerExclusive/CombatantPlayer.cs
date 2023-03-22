using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatantPlayer : CombativeRobot
{
    private static readonly float JUMP_MAGNITUDE = 5f;
    private static readonly float FALLING_VELOCITY_THRESHOLD = -1f;

    [SerializeField] private Transform testTarget;

    private Rigidbody rigidbody;

    private bool _isFalling = false;
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
        if (!_isFalling && !_isMovementBlocked)
        {
            OnPlayerWalk();
            OnPlayerJump();
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

    public void OnReceiveDamage(int amount)
    {
        base.OnReceiveDamage(amount);
        // TODO: Implementar animaciones de golpes
    }
}
