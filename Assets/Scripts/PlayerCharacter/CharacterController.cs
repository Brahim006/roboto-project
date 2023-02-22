using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    private static readonly float WALK_SPEED = 2f;
    private static readonly float RUN_SPEED = 4f;
    private static readonly float JUMP_MAGNITUDE = 5f;
    private static readonly float PRESS_BUTTON_ANIMATION_LENGTH = 2f;

    private PlayerState playerState = PlayerState.Idle;
    private Rigidbody rigidbody;
    private Animator animator;

    private bool _isActionBlocked = false;
    private bool _alternativeCameraOn = false;
    private float _pressButtonOffset;
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        animator = gameObject.GetComponentInChildren<Animator>();
        _pressButtonOffset = PRESS_BUTTON_ANIMATION_LENGTH;
    }

    // Update is called once per frame
    void Update()
    {
        OnAnimationSwitch();
        if(!_isActionBlocked)
        {
            OnSwitchCamera();
            OnPlayerWalk();
            OnPlayerJump();
        }
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
            rigidbody.AddForceAtPosition(Vector3.up * JUMP_MAGNITUDE, transform.position, ForceMode.Impulse);
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
                _isActionBlocked = false;
            }
        }
    }
    public void PressButton(Vector3 buttonDirection)
    {
        if(playerState == PlayerState.Idle)
        {
            _isActionBlocked = true;
            var angle = Vector3.Angle(transform.forward, buttonDirection) - 30;
            transform.Rotate(Vector3.up, angle);
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
            case PlayerState.Falling:
                if (animator.GetInteger("animation") != 3) animator.SetInteger("animation", 3);
                break;
            case PlayerState.PressingButton:
                if (animator.GetInteger("animation") != 4) animator.SetInteger("animation", 4);
                break;
            default:
                if (animator.GetInteger("animation") != 0) animator.SetInteger("animation", 0);
                break;
        }
    }
}

enum PlayerState
{
    Idle,
    Walking,
    Running,
    Falling,
    PressingButton
}
