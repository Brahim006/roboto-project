using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    private static readonly float WALK_SPEED = 2f;
    private static readonly float RUN_SPEED = 4f;
    private static readonly float JUMP_MAGNITUDE = 5f;
    private static readonly float PRESS_BUTTON_ANIMATION_LENGTH = 2f;
    private static readonly float FALLING_VELOCITY_THRESHOLD = -1f;
    private static readonly int INITIAL_HEALTH = 100;

    private PlayerState playerState = PlayerState.Idle;
    private Rigidbody rigidbody;
    private Animator animator;
    private int health;

    private bool _alternativeCameraOn = false;
    private float _pressButtonOffset;
    void Start()
    {
        health = INITIAL_HEALTH;
        rigidbody = GetComponent<Rigidbody>();
        animator = gameObject.GetComponentInChildren<Animator>();
        _pressButtonOffset = PRESS_BUTTON_ANIMATION_LENGTH;
    }

    // Update is called once per frame
    void Update()
    {
        OnAnimationSwitch();
        if(playerState != PlayerState.PressingButton)
        {
            OnSwitchCamera();
            OnPlayerWalk();
            OnPlayerJump();
        }
        CheckForFallingState();
        OnPlayerPressingButton();
    }

    private void OnPlayerWalk()
    {
        var l_vertical = Input.GetAxisRaw("Vertical");
        var l_horizontal = Input.GetAxisRaw("Horizontal");

        if (l_vertical != 0 || l_horizontal != 0)
        {
            float speed;
            if(Input.GetKey(KeyCode.LeftShift))
            {
                speed = RUN_SPEED;
                playerState = PlayerState.Running;
            }
            else
            {
                speed = WALK_SPEED;
                playerState = PlayerState.Walking;
            }
            var l_movementDirection =
                _alternativeCameraOn ?
                new Vector3(-1 * l_vertical, 0, l_horizontal)
                : new Vector3(l_horizontal, 0, l_vertical);

            transform.LookAt(transform.position + l_movementDirection);
            transform.position += l_movementDirection * speed * Time.deltaTime;
        } else if(playerState == PlayerState.Walking || playerState == PlayerState.Running)
        {
            playerState = PlayerState.Idle;
        }
    }

    private void OnSwitchCamera()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            _alternativeCameraOn = !_alternativeCameraOn;
        }
    }

    private void OnPlayerJump()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Physics.Raycast(transform.position + Vector3.up, Vector3.down, out RaycastHit hitInfo);
            if(hitInfo.distance < 1.1)
            {
                playerState = PlayerState.Jumping;
                rigidbody.AddForceAtPosition(Vector3.up * JUMP_MAGNITUDE, transform.position, ForceMode.Impulse);
            }
        }
    }

    private void CheckForFallingState()
    {
        var velocity = rigidbody.velocity.y;
        if(rigidbody.velocity.y < FALLING_VELOCITY_THRESHOLD)
        {
            playerState = PlayerState.Falling;
        }
        else if (playerState == PlayerState.Falling && rigidbody.velocity.y == 0)
        {
            playerState = PlayerState.Idle;
        }

    }

    private void OnPlayerPressingButton()
    {
        if(playerState == PlayerState.PressingButton)
        {
            _pressButtonOffset -= Time.deltaTime;
            if(_pressButtonOffset <= 0)
            {
                playerState = PlayerState.Idle;
                _pressButtonOffset = PRESS_BUTTON_ANIMATION_LENGTH;
            }
        }
    }
    public void PressButton(Vector3 buttonDirection)
    {
        if(playerState != PlayerState.PressingButton)
        {
            Vector3 lookAtPoint = new Vector3(buttonDirection.x, transform.position.y, buttonDirection.z);
            transform.LookAt(lookAtPoint);
            playerState = PlayerState.PressingButton;
        }
    }

    private void OnAnimationSwitch()
    {
        switch(playerState)
        {
            case PlayerState.Walking:
                if(animator.GetInteger("animation") != 1) animator.SetInteger("animation", 1);
                break;
            case PlayerState.Running:
                if (animator.GetInteger("animation") != 2) animator.SetInteger("animation", 2);
                break;
            case PlayerState.Jumping:
                if (animator.GetInteger("animation") != 3) animator.SetInteger("animation", 3);
                break;
            case PlayerState.Falling:
                if (animator.GetInteger("animation") != 4) animator.SetInteger("animation", 4);
                break;
            case PlayerState.Landing:
                if (animator.GetInteger("animation") != 5) animator.SetInteger("animation", 5);
                break;
            case PlayerState.PressingButton:
                if (animator.GetInteger("animation") != 6) animator.SetInteger("animation", 6);
                break;
            default:
                if (animator.GetInteger("animation") != 0) animator.SetInteger("animation", 0);
                break;
        }
    }

    // Health management methods
    public void OnReceiveDamage(int amount)
    {
        var newHealth = health;
        newHealth -= amount;
        if(newHealth <0) newHealth = 0;
        health = newHealth;
    }
}

enum PlayerState
{
    Idle,
    Walking,
    Running,
    Jumping,
    Falling,
    Landing,
    PressingButton
}
