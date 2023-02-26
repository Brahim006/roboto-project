using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

public class RobotGuardController : MonoBehaviour
{
    private static readonly float PUSH_INTENSITY = 5f;

    private Animator animator;
    private GuardState guardState = GuardState.Idle;

    private Vector3 _originalForward;
    void Start()
    {
        _originalForward = transform.forward;
        animator = GetComponent<Animator>();
        var triggerCollider = GetComponent<CapsuleCollider>();
        triggerCollider.center += transform.forward * 0.05f;
    }

    // Update is called once per frame
    void Update()
    {
        OnAnimationChange();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            guardState = GuardState.GuardIdle;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            var l_guardLookPoint = other.transform.position;
            l_guardLookPoint.y = transform.position.y;
            transform.LookAt(l_guardLookPoint);
            var distance = Vector3.Distance(transform.position, other.transform.position);
            if(distance <= 0.5f && guardState != GuardState.HeadbuttPush)
            {
                guardState = GuardState.HeadbuttPush;
                var l_playerLookPoint = transform.position;
                l_playerLookPoint.y = other.transform.position.y;
                other.attachedRigidbody.AddForce(transform.forward + Vector3.up * PUSH_INTENSITY, ForceMode.Impulse);
                other.gameObject.GetComponent<CharacterController>().OnReceiveDamage(5);
            }
            else if(guardState != GuardState.GuardIdle)
            {
                guardState = GuardState.GuardIdle;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            transform.LookAt(_originalForward);
            guardState = GuardState.Idle;
        }
    }
    private void OnAnimationChange()
    {
        switch(guardState)
        {
            case GuardState.GuardIdle:
                if (animator.GetInteger("animation") != 1) animator.SetInteger("animation", 1);
                break;
            case GuardState.HeadbuttPush:
                if (animator.GetInteger("animation") != 2) animator.SetInteger("animation", 2);
                break;
            default:
                if (animator.GetInteger("animation") != 0) animator.SetInteger("animation", 0);
                break;
        }
    }
}

enum GuardState
{
    Idle,
    GuardIdle,
    HeadbuttPush
}