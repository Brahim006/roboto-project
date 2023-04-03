using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetalDoor : MonoBehaviour
{
    private static readonly float OPEN_HEIGTH = 0.6f;

    private bool _isOpening = false;

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
            _isOpening = false;
            GameObject.FindObjectOfType<GameManager>().TransitionFromTutorialToCity();
        }
    }
    public void OpenDoor()
    {
        _isOpening= true;
    }
}
