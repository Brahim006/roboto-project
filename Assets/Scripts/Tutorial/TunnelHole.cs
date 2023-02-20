using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TunnelHole : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("destructable-object"))
        {
            Destroy(other.gameObject);
        }
    }
}
