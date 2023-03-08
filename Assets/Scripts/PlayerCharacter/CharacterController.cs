using System;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.Events;
using UnityEngine;


public class CharacterController : RobotWithHealt
{
    private static readonly float WALK_SPEED = 2f;
    private static readonly float RUN_SPEED = 4f;
    private static readonly float JUMP_MAGNITUDE = 5f;
    private static readonly float PRESS_BUTTON_ANIMATION_LENGTH = 2f;
    private static readonly float FALLING_VELOCITY_THRESHOLD = -1f;

    private PlayerState playerState = PlayerState.Idle;
    private Rigidbody rigidbody;
    private Animator animator;

    private float _forwardRotationAngle;
    private float _pressButtonOffset;
    private bool _isActionBlocked = false;

    public UnityEvent <bool> StartToWalking;
    public UnityEvent<bool> StartToRunnig;
    public UnityEvent<bool> StopMoving;

    [SerializeField] private CameraController cameraController;
    protected override void Start()
    {
        base.Start();
        rigidbody = GetComponent<Rigidbody>();
        animator = gameObject.GetComponentInChildren<Animator>();
        _pressButtonOffset = PRESS_BUTTON_ANIMATION_LENGTH;
        cameraController.OnChangeFoward += ChangeForwardDirection;
    }

    // Update is called once per frame
    void Update()
    {
        OnAnimationSwitch();
        if(!_isActionBlocked)
        {
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
                if (playerState != PlayerState.Running) StartToRunnig?.Invoke(true);
                playerState = PlayerState.Running;
            }
            else
            {
                speed = WALK_SPEED;
                if (playerState != PlayerState.Walking) StartToWalking?.Invoke(true);
                playerState = PlayerState.Walking;
            }

            var l_movementDirection = new Vector3(l_horizontal, 0, l_vertical);
            l_movementDirection = Quaternion.AngleAxis(_forwardRotationAngle, Vector3.up) * l_movementDirection;

            transform.LookAt(transform.position + l_movementDirection);
            transform.position += l_movementDirection * speed * Time.deltaTime;
        } else if(playerState == PlayerState.Walking || playerState == PlayerState.Running)
        {
            playerState = PlayerState.Idle;
            StopMoving?.Invoke(true);
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
                _isActionBlocked = false;
            }
        }
    }

    public void ChangeForwardDirection(Vector3 newForward)
    {
        _forwardRotationAngle = Vector3.SignedAngle(Vector3.forward, newForward, Vector3.up);
        Debug.Log(_forwardRotationAngle);
    }
    public void PressButton(Vector3 buttonDirection)
    {
        if(playerState != PlayerState.PressingButton)
        {
            Vector3 lookAtPoint = new Vector3(buttonDirection.x, transform.position.y, buttonDirection.z);
            transform.LookAt(lookAtPoint);
            playerState = PlayerState.PressingButton;
            _isActionBlocked = true;
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
