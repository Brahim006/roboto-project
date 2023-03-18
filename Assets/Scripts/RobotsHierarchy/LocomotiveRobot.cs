using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public abstract class LocomotiveRobot : RobotWithSounds
{
    private static readonly float FORWARD_SPEED = 3f;
    private static readonly float BACKWARDS_SPEED = 1.5f;
    private static readonly float ROTATION_SPEED = 90f;
    private static readonly float LERP_INTENSITY = 3.0f;

    protected Animator animator;

    private Vector3 _lookAt;
    private bool _isLerping;
    protected virtual void Start()
    {
        base.Start();
        animator = gameObject.GetComponentInChildren<Animator>();
        _isLerping = false;
        _lookAt = transform.position;
    }

    protected virtual void Update()
    {
        if (_isLerping)
        {
            OnRobotLerping();
        }
    }
    protected void OnRobotMove(float verticalAxis, float horizontalAxis)
    {
        animator.SetFloat("vertical", verticalAxis);
        animator.SetFloat("horizontal", horizontalAxis);

        if (verticalAxis != 0)
        {
            transform.position += 
                transform.forward *
                verticalAxis *
                Time.deltaTime *
                (verticalAxis > 0 ? FORWARD_SPEED : BACKWARDS_SPEED);
        }

        if (horizontalAxis != 0)
        {
            transform.Rotate(Vector3.up, horizontalAxis * ROTATION_SPEED * Time.deltaTime);
        }
    }

    private void OnRobotLerping()
    {
        Vector3 l_planeLookAt = new Vector3(_lookAt.x, transform.position.y, _lookAt.z);
        Quaternion l_NewRotation = Quaternion.LookRotation(l_planeLookAt - transform.position);
        transform.rotation = Quaternion.Lerp(transform.rotation, l_NewRotation, LERP_INTENSITY * Time.deltaTime);
    }

    protected void LookAtTarget(Vector3 lerpTo)
    {
        _isLerping = true;
        _lookAt = lerpTo;
    }

    protected void StopLookingAt()
    {
        _isLerping = false;
        _lookAt = transform.position;
    }
}


