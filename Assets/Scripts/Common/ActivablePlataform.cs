using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivablePlataform : MonoBehaviour
{
    private Animator animator;
    private AudioSource audioSource;
    protected virtual void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    public virtual void OnToggleActive () 
    {
        bool isActive = animator.GetBool("isActive");
        audioSource.Play();
        animator.SetBool("isActive", !isActive);
    }
}
