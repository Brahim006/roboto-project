using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CombatantPlayer : CombativeRobot
{
    private static readonly float JUMP_MAGNITUDE = 5f;
    private static readonly float FALLING_VELOCITY_THRESHOLD = -1f;
    private static readonly int HIT_BASE_POWER = 10;

    private Rigidbody rigidbody;
    private AudioSource jumpAudioSource;
    private List<CombativeGuard> enemiesNearby;

    private bool _isFalling = false;
    private bool _isJumping = false;
    private bool _isDead = false;
    protected override void Start()
    {
        base.Start();
        rigidbody = GetComponent<Rigidbody>();
        jumpAudioSource = GetComponents<AudioSource>()[1];
        enemiesNearby = new List<CombativeGuard>();
        GameObject.FindObjectOfType<HUDManager>()?.AssignPlayer(this);
        OnDeath += OnPlayerDeath;
    }

    protected override void Update()
    {
        base.Update();
        CheckForDeadState();
        CheckForFallingState();
        if (!_isFalling && !_isDead)
        {
            if(!_isMovementBlocked)
            {
                OnPlayerWalk();
                if(!_isJumping)
                {
                    OnPlayerJump();
                }
            }
            // Fighting exclusive
            if(target != null)
            {
                OnChangeTarget();
                OnPlayerAttack();
                checkForBlockingState();
            }
        }
    }

    private void OnPlayerWalk()
    {
        var l_vertical = Input.GetAxisRaw("Vertical");
        if (target is null && l_vertical != 0)
        {
            OnRobotMove(
            Input.GetAxisRaw("Vertical"),
            Input.GetAxis("Horizontal"),
            !_isJumping
            );
        }
        else
        {
            OnRobotMove(
            Input.GetAxisRaw("Vertical"),
            Input.GetAxisRaw("Horizontal"),
            !_isJumping
            );
        }

    }

    private void OnPlayerJump()
    {
        if (!_isFalling && Input.GetKeyDown(KeyCode.Space))
        {
            if(target is null)
            {
                var l_vertical = Input.GetAxisRaw("Vertical");
                Vector3 l_jumpDirection = (transform.forward * l_vertical) + Vector3.up;
                rigidbody.AddForceAtPosition(l_jumpDirection * JUMP_MAGNITUDE, transform.position, ForceMode.Impulse);
                _isJumping = true;
                jumpAudioSource.Play();
                animator.SetTrigger("jumps");
            }
        }
    }
    private void OnPlayerAttack()
    {
        if (Input.GetMouseButtonDown(0))
        {
            OnRobotAttack();
            bool successfullHit = Physics.Raycast(
            transform.position + Vector3.up,
            transform.forward,
            out RaycastHit hitInfo,
            HIT_DISTANCE
            );
            
            if (successfullHit && hitInfo.transform.gameObject.TryGetComponent<CombativeGuard>(out CombativeGuard enemy))
            {
                enemy.OnReceiveDamage(HIT_BASE_POWER * lightAttackIndex);
            }
        }
    }

    private void CheckForFallingState()
    {
        if (rigidbody.velocity.y < FALLING_VELOCITY_THRESHOLD)
        {
            // Está cayendo
            _isFalling = true;
            animator.SetBool("isFalling", true);
        }
        else
        {
            // No está cayendo
            if (_isFalling && _isJumping)
            {
                _isJumping = false;
                jumpAudioSource.Play();
            }
            _isFalling = false;
            animator.SetBool("isFalling", false);
        }
    }
    private void checkForBlockingState()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            ToggleBlocking(true);
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            ToggleBlocking(false);
        }
    }

    private void CheckForDeadState()
    {
        if(_isDead)
        {
            if(animator.GetCurrentAnimatorStateInfo(_combatLayerIndex).normalizedTime >= 0.9)
            {
                GameObject.FindObjectOfType<GameManager>().RespawnPlayer();
                _isDead = false;
                ReceiveHealing(100);
            }
        }
    }

    private void OnPlayerDeath()
    {
        //animator.SetTrigger("die");
        _isBlocking = false;
        _isMovementBlocked = false;
        _isDead = true;
    }
    private void OnChangeTarget()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            ReTarget();
        }
    }

    private void ReTarget()
    {
        var newTarget = enemiesNearby.Find(enemy => enemy != target);
        SetTarget(newTarget);
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Enemy"))
        {
            var enemy = other.GetComponent<CombativeGuard>();
            if(target is null)
            {
                SetTarget(enemy);
            }
            enemy.OnDeath += OnKillEnemy;
            enemiesNearby.Add(enemy);
            enemy.SetTarget(this);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            var enemy = other.GetComponent<CombativeGuard>();
            enemiesNearby.Remove(enemy);
            if (enemiesNearby.Count == 0)
            {
                UnTarget();
            }
            else if(enemy == target)
            {
                ReTarget();
            }
        }
    }

    private void OnKillEnemy()
    {
        target.OnDeath -= OnKillEnemy;
        enemiesNearby.Remove(target as CombativeGuard);
        if (enemiesNearby.Count == 0)
        {
            UnTarget();
            _isMovementBlocked = false;
        }
        else
        {
            ReTarget();
        }
    }

    public void ReceiveHealing(int amount)
    {
        OnHeal(amount);
    }
}
