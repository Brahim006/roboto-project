using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

public class RobotGuardController : PlayerSeeker
{
    private static readonly float PUSH_INTENSITY = 5f;

    private GuardState guardState = GuardState.Idle;


    protected override void Start()
    {
        base.Start();
        var triggerCollider = GetComponent<CapsuleCollider>();
        triggerCollider.center += transform.forward * 0.05f;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        OnAnimationChange();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            guardState = GuardState.GuardIdle;
            SeekPlayer();
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            
            var distance = Vector3.Distance(transform.position, other.transform.position);
            if(distance <= 0.5f && guardState != GuardState.HeadbuttPush)
            {
                guardState = GuardState.HeadbuttPush;
                var l_playerLookPoint = transform.position;
                l_playerLookPoint.y = player.transform.position.y;
                var playerRigidbody = player.GetComponent<Rigidbody>();
                playerRigidbody.velocity = new Vector3(0, 0, 0);
                playerRigidbody.AddForce((other.transform.forward * -1) + Vector3.up * PUSH_INTENSITY, ForceMode.Impulse);
                player.OnReceiveDamage(5);
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
            StopSeekingPlayer();
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