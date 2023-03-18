using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class PlataformerPlayer : LocomotiveRobot
{
    private static readonly float JUMP_MAGNITUDE = 5f;
    private static readonly float PRESS_BUTTON_ANIMATION_LENGTH = 2f;
    private static readonly float FALLING_VELOCITY_THRESHOLD = -1f;

    private Rigidbody rigidbody;

    private bool _isFalling = false;
    private float _pressButtonOffset;
    private bool _isActionBlocked = false;
    protected override void Start()
    {
        base.Start();
        rigidbody = GetComponent<Rigidbody>();
        _pressButtonOffset = PRESS_BUTTON_ANIMATION_LENGTH;
    }

    protected void Update()
    {
        CheckForFallingState();
        if(!_isFalling)
        {
            OnPlayerWalk();
            OnPlayerJump();
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

    private void OnPlayerPressingButton()
    {
        /*if (animationState == AnimationState.PressingButton)
        {
            _pressButtonOffset -= Time.deltaTime;
            if (_pressButtonOffset <= 0)
            {
                animationState = AnimationState.Idle;
                _pressButtonOffset = PRESS_BUTTON_ANIMATION_LENGTH;
                _isActionBlocked = false;
            }
        }*/
    }

    public void PressButton(Vector3 buttonDirection)
    {
        /*if (animationState != AnimationState.PressingButton)
        {
            Vector3 lookAtPoint = new Vector3(buttonDirection.x, transform.position.y, buttonDirection.z);
            transform.LookAt(lookAtPoint);
            animationState = AnimationState.PressingButton;
            _isActionBlocked = true;
        }*/
    }

    public void OnReceiveDamage(int amount)
    {
        base.OnReceiveDamage(amount);
    }
}