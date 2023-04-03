using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivablePlataform : MonoBehaviour
{
    private Animator animator;
    protected virtual void Start()
    {
        animator = GetComponent<Animator>();
    }

    public virtual void OnToggleActive () 
    {
        animator.SetBool("isActive", !animator.GetBool("isActive"));
    }
}
