using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

public class RobotGuardController : PlayerSpotter
{
    private static readonly float PUSH_INTENSITY = 10f;

    private GuardState guardState = GuardState.Idle;
    protected override void Start()
    {
        base.Start();
        var triggerCollider = GetComponent<CapsuleCollider>();
        triggerCollider.center += transform.forward * 0.05f;
    }

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
            SpotPlayer();
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            var distance = Vector3.Distance(transform.position, player.transform.position);
            if(distance <= 0.5f && guardState != GuardState.HeadbuttPush)
            {
                guardState = GuardState.HeadbuttPush;
                var l_playerLookPoint = transform.position;
                l_playerLookPoint.y = player.transform.position.y;
                var l_playerRB = player.GetComponent<Rigidbody>();
                l_playerRB.AddForceAtPosition(
                    (player.transform.forward * -1) * PUSH_INTENSITY,
                    player.transform.position,
                    ForceMode.Impulse
                    );
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
            guardState = GuardState.Idle;
            UnSpotlayer();
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