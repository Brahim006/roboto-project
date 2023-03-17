using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlataformerPlayer : LocomotiveRobot
{
    private static readonly float JUMP_MAGNITUDE = 5f;
    private static readonly float PRESS_BUTTON_ANIMATION_LENGTH = 2f;
    private static readonly float FALLING_VELOCITY_THRESHOLD = -1f;

    private Rigidbody rigidbody;

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
        OnPlayerWalk();
        OnPlayerJump();
    }

    private void OnPlayerWalk()
    {
        OnRobotMove(
            Input.GetAxisRaw("Vertical"),
            Input.GetAxisRaw("Horizontal")
            );
    }

    private void OnPlayerJump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Physics.Raycast(transform.position + Vector3.up, Vector3.down, out RaycastHit hitInfo);
            if (hitInfo.distance < 1.1)
            {
                rigidbody.AddForceAtPosition(Vector3.up * JUMP_MAGNITUDE, transform.position, ForceMode.Impulse);
            }
        }
    }

    private void CheckForFallingState()
    {
        var velocity = rigidbody.velocity.y;
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