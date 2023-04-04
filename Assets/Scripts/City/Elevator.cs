using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    private static readonly float MOVEMENT_SPEED = 1.0f;
    [SerializeField] private float higherHeight;
    [SerializeField] private float lowerHeight;
    [SerializeField] private MovementDirection currentDirection;
    private AudioSource audioSource;

    private bool _isActive = false;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    void Update()
    {
        if(_isActive)
        {
            if(currentDirection == MovementDirection.Up)
            {
                transform.position += Vector3.up * MOVEMENT_SPEED * Time.deltaTime;
                if(transform.position.y >= higherHeight)
                {
                    currentDirection = MovementDirection.Down;
                }

            }
            else
            {
                transform.position += Vector3.down * MOVEMENT_SPEED * Time.deltaTime;
                if (transform.position.y <= lowerHeight)
                {
                    currentDirection = MovementDirection.Up;
                }
            }
        }
    }
    public void ToggleActive()
    {
        _isActive = !_isActive;
        if(_isActive)
        {
            audioSource.Play();
        }
        else
        {
            audioSource.Stop();
        }
    }
}

enum MovementDirection
{
    Up,
    Down
}
