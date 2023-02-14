using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    private static readonly float MOVEMENT_SPEED = 2f;
    private static readonly float ROTATION_SPEED = 2f;
    private static readonly float JUMP_MAGNITUDE = 5f;
    private Rigidbody rigidbody;
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        OnPlayerMove();
        OnPlayerJump();
    }

    private void OnPlayerMove()
    {
        var l_vertical = Input.GetAxisRaw("Vertical");
        var l_horizontal = Input.GetAxisRaw("Horizontal");
        if(l_vertical != 0)
        {
            transform.position += transform.forward * l_vertical * MOVEMENT_SPEED * Time.deltaTime;
        }
        if(l_horizontal != 0)
        {
            transform.Rotate(Vector3.up, l_horizontal * ROTATION_SPEED);
        }
    }

    private void OnPlayerJump()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        { 
            rigidbody.AddForceAtPosition(Vector3.up * JUMP_MAGNITUDE, transform.position, ForceMode.Impulse);
        }
    }
}
