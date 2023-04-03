using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.Playables;

public class PlataformerPlayer : LocomotiveRobot
{
    private static readonly float JUMP_MAGNITUDE = 5f;
    private static readonly float ACTION_ANIMATION_LENGTH = 3f;
    private static readonly float FALLING_VELOCITY_THRESHOLD = -1f;

    private Rigidbody rigidbody;

    private bool _isFalling = false;
    private float _actionffset;
    private bool _isActionBlocked = false;
    protected override void Start()
    {
        base.Start();
        rigidbody = GetComponent<Rigidbody>();
        _actionffset = ACTION_ANIMATION_LENGTH;
    }

    protected override void Update()
    {
        base.Update();
        OnPlayerPerformingAction();
        if (!_isActionBlocked)
        {
            CheckForFallingState();
            if(!_isFalling)
            {
                OnPlayerWalk();
                OnPlayerJump();
            }
        }
    }

    private void OnPlayerWalk()
    {
        var l_vertical = Input.GetAxisRaw("Vertical");
        if(l_vertical != 0)
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
            var l_vertical = Input.GetAxisRaw("Vertical");
            Vector3 l_jumpDirection = (transform.forward * l_vertical) + Vector3.up;
            rigidbody.AddForceAtPosition(l_jumpDirection * JUMP_MAGNITUDE, transform.position, ForceMode.Impulse);
            animator.SetTrigger("jumps");
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

    private void OnPlayerPerformingAction()
    {
        _actionffset -= Time.deltaTime;
        if (_actionffset <= 0)
        {
            _actionffset = ACTION_ANIMATION_LENGTH;
            _isActionBlocked = false;
            StopLookingAt();
        }
    }

    public void Stomp(Vector3 direction)
    {
        _isActionBlocked = true;
        LookAtTarget(direction);
        animator.SetTrigger("stomping");
    }

    public void PressButton(Vector3 buttonDirection)
    {
        _isActionBlocked = true;
        LookAtTarget(buttonDirection);
        animator.SetTrigger("pressButton");
    }

    public void OnReceiveDamage(int amount)
    {
        base.OnReceiveDamage(amount);
    }
}