using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivablePlataform : MonoBehaviour
{
    private Animator animator;
    void Start()
    {
        animator= GetComponent<Animator>();
    }

    public void OnToggleActive () 
    {
        animator.SetBool("isActive", !animator.GetBool("isActive"));
    }
}
