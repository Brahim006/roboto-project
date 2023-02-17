using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    private static readonly float WALK_SPEED = 2f;
    private static readonly float JUMP_MAGNITUDE = 5f;
    private Rigidbody rigidbody;
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        OnPlayerWalk();
        OnPlayerJump();
    }

    private void OnPlayerWalk()
    {
        var l_vertical = Input.GetAxisRaw("Vertical");
        var l_horizontal = Input.GetAxisRaw("Horizontal");

        if (l_vertical != 0 || l_horizontal != 0)
        {
            var l_movementDirection = new Vector3(l_horizontal, 0, l_vertical);
            transform.position += l_movementDirection * WALK_SPEED * Time.deltaTime;
            transform.LookAt(transform.position + l_movementDirection);
        }
    }

    private void OnPlayerJump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Physics.Raycast(transform.position, Vector3.up * -1, out RaycastHit hitInfo);
            if (hitInfo.distance < 0.1)
            {
                rigidbody.AddForceAtPosition(Vector3.up * JUMP_MAGNITUDE, transform.position, ForceMode.Impulse);
            }
        }
    }
}
