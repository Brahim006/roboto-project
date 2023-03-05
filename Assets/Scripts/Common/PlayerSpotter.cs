using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerSpotter : MonoBehaviour
{
    private static readonly float LERP_INTENSITY = 3.0f;

    [SerializeField] protected CharacterController player;
    protected Animator animator;

    protected Vector3 _originalForward;
    private bool _isSpottingPlayer = false;
    protected virtual void Start()
    {
        _originalForward = transform.forward;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if(_isSpottingPlayer)
        {
            OnSpottingPlayer();
        }
        else
        {
            OnStandingIdle();
        }
    }

    private void OnSpottingPlayer()
    {
        Vector3 l_PlayerPlanePosition = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
        Quaternion l_NewRotation = Quaternion.LookRotation(l_PlayerPlanePosition - transform.position);
        transform.rotation = Quaternion.Lerp(transform.rotation, l_NewRotation, LERP_INTENSITY * Time.deltaTime);
    }

    private void OnStandingIdle()
    {
        if(transform.forward != _originalForward)
        {
            Quaternion l_NewRotation = Quaternion.LookRotation(_originalForward - transform.position);
            transform.rotation = Quaternion.Lerp(transform.rotation, l_NewRotation, LERP_INTENSITY * Time.deltaTime);
        }
    }

    protected void SpotPlayer()
    {
        _isSpottingPlayer = true;
    }

    protected void UnSpotlayer()
    {
        _isSpottingPlayer = false;
    }
}
