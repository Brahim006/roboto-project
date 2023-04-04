using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetalDoor : MonoBehaviour
{
    private static readonly float OPEN_HEIGTH = 0.6f;
    private AudioSource audioSource;

    private bool _isOpening = false;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    void Update()
    {
        if(_isOpening)
        {
            CheckForOpeningState();
        }
    }

    private void CheckForOpeningState()
    {
        transform.position += Vector3.down * Time.deltaTime;
        if (transform.position.y <= OPEN_HEIGTH)
        {
            audioSource.Stop();
            _isOpening = false;
            GameObject.FindObjectOfType<GameManager>().TransitionFromTutorialToCity();
        }
    }
    public void OpenDoor()
    {
        _isOpening= true;
        audioSource.Play();
    }
}
