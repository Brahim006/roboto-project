using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatantModel : MonoBehaviour
{
    [SerializeField] private EnterFactoryCinematics cinematicManager;
    private Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        var currenAnimInfo = animator.GetCurrentAnimatorStateInfo(0);
        if (currenAnimInfo.IsName("LookAtArm"))
        {
            if(currenAnimInfo.normalizedTime <= 0.1)
            {
                cinematicManager.ActivateHUD();
            }
            else if(currenAnimInfo.normalizedTime >= 0.9)
            {
                cinematicManager.EndCinematic();
            }
        }
    }
}
