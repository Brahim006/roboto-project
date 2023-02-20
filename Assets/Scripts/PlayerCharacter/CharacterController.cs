using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    private static readonly float WALK_SPEED = 2f;
    private static readonly float JUMP_MAGNITUDE = 1.5f;

    private PlayerState playerState = PlayerState.Idle;
    private Rigidbody rigidbody;
    private Light headLight;
    private Animator animator;

    private bool _alternativeCameraOn = false;
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        animator = gameObject.GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        OnAnimationSwitch();
        OnSwitchCamera();
        OnPlayerWalk();
    }

    private void OnPlayerWalk()
    {
        var l_vertical = Input.GetAxisRaw("Vertical");
        var l_horizontal = Input.GetAxisRaw("Horizontal");

        if (l_vertical != 0 || l_horizontal != 0)
        {
            playerState = PlayerState.Walking;
            var l_movementDirection =
                _alternativeCameraOn ?
                new Vector3(-1 * l_vertical, 0, l_horizontal)
                : new Vector3(l_horizontal, 0, l_vertical);
            transform.position += l_movementDirection * WALK_SPEED * Time.deltaTime;
            transform.LookAt(transform.position + l_movementDirection);
        } else if(playerState == PlayerState.Walking)
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

    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKeyDown(KeyCode.Space) && playerState != PlayerState.Falling)
        {
            rigidbody.AddForceAtPosition(Vector3.up * JUMP_MAGNITUDE, transform.position, ForceMode.Impulse);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(playerState == PlayerState.Falling) playerState= PlayerState.Idle;
    }

    private void OnTriggerExit(Collider other)
    {
        playerState= PlayerState.Falling;
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
            default:
                if (animator.GetInteger("animation") != 0) animator.SetInteger("animation", 0);
                break;
        }
    }

    private void OnSwitchCamera()
    {
        if(Input.GetKeyDown(KeyCode.Tab)) 
        { 
            alternativeCameraOn = !alternativeCameraOn;
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