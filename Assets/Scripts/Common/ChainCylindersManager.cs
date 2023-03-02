using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainCylindersManager : MonoBehaviour
{
    private static readonly float ROTATION_SPEED = 30f;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up, ROTATION_SPEED * Time.deltaTime);
    }
}
