using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoboticWelder : MonoBehaviour
{
    private static readonly float ANIMATION_DURATION = 11.958f;
    private float SPARKS_ENABLING_NORMALIZED_TIME = 0.625f;
    private float SPARKS_DISABLING_NORMALIZED_TIME = 0.88f;
    
    private Animator animator;
    [SerializeField] private GameObject sparks;
    [SerializeField] private GameObject disposedArm;
    [SerializeField] private EnterFactoryCinematics cinematicManager;
    private float _animationOffset = ANIMATION_DURATION;
    private float _totalDuration = ANIMATION_DURATION * 2;
    void Start()
    {
        animator = GetComponent<Animator>();
        animator.enabled = false;
    }

    void Update()
    {
        if(animator.enabled)
        {
            var normalizedTime = animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
            if(normalizedTime >= SPARKS_ENABLING_NORMALIZED_TIME &&
                normalizedTime < SPARKS_DISABLING_NORMALIZED_TIME)
            {
                sparks.SetActive(true);
                cinematicManager.MakeRobotSuffer();
                if (!disposedArm.active)
                {
                    disposedArm.SetActive(true);
                    disposedArm.GetComponent<Rigidbody>().AddForce(
                        (Vector3.up + Vector3.forward - Vector3.right) * 7,
                        ForceMode.Impulse
                        );
                }
            }
            else if(normalizedTime >= SPARKS_DISABLING_NORMALIZED_TIME)
            {
                sparks.SetActive(false);
                cinematicManager.EndRobotsPain();
                if(SPARKS_DISABLING_NORMALIZED_TIME > 1)
                {
                    cinematicManager.TogleLowerCamera(false);
                }
            }
            _animationOffset -= Time.deltaTime;
            _totalDuration -= Time.deltaTime;
            if(_animationOffset <= 0)
            {
                _animationOffset = ANIMATION_DURATION;
                SPARKS_ENABLING_NORMALIZED_TIME += 1;
                SPARKS_DISABLING_NORMALIZED_TIME += 1;
            }
            if(_totalDuration <= 0)
            {
                animator.enabled = false;
                cinematicManager.SpawnCombatantModel();
            }
        }
    }

    public void PlayAnimation()
    {
        animator.enabled = true;
    }
}
