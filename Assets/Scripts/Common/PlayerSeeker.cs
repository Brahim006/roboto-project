using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSeeker : MonoBehaviour
{
    [SerializeField] protected CharacterController player;
    protected Animator animator;
    protected Vector3 _originalForward;
    private bool _isSeekingPlayer = false;
    

    protected virtual void Start()
    {
        animator = GetComponent<Animator>();
        _originalForward = transform.forward;
    }

    protected virtual void Update()
    {
        if (_isSeekingPlayer)
        {
            OnSeekingPlayer();
        }
        else
        {
            OnLookingForward();
        }
    }

    private void OnSeekingPlayer()
    {
        Vector3 lookAtVector = new Vector3(
            player.transform.position.x,
            transform.position.y,
            player.transform.position.z
        );
        transform.LookAt(lookAtVector);

    }
    private void OnLookingForward()
    {
        if(transform.forward != _originalForward)
        {
        transform.LookAt(_originalForward);
        }
    }
    protected void SeekPlayer()
    {
        _isSeekingPlayer = true;
    }
    protected void StopSeekingPlayer()
    {
        _isSeekingPlayer = false;
    }
}
