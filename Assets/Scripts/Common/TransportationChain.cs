using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransportationChain : MonoBehaviour
{
    private static readonly float TRANSPORT_SPEED = 0.5f;

    private void OnTriggerStay(Collider other)
    {
        if (!other.isTrigger)
        {
            other.transform.position += Vector3.left * TRANSPORT_SPEED * Time.fixedDeltaTime;
        }
    }
}
