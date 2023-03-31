using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GridBrushBase;

public class CombativeGuard : CombativeRobot
{
    private static readonly float ACTION_TRIGGER_DISTANCE = 2f;
    private static readonly float ATTACK_PROBABILITY_RANGE = 2.5f;
    private static readonly float BLOCK_PROBABILITY_RANGE = 5f;
    private static readonly float ROTATE_AROUND_PROBABILITY_RANGE = 7.5f;
    private static readonly float MIN_ACTION_TIME = 2f;
    private static readonly float MAX_ACTION_TIME = 4f;
    private static readonly float TIME_BETWEEN_ATTACKS = 0.95f;
    private static readonly int MAX_ATTACKS_PER_ACTION = 4;
    private static readonly int HIT_BASE_POWER = 5;

    private FightingActionState fightingActionState = FightingActionState.Pursuing;
    [SerializeField] DeadEnemiesPrefabs deadAnimationPrefabs;
    [SerializeField] Vector3[] patrollingCheckpoints = new Vector3[4];

    private Vector3 _currentPatrollingPoint;
    private float _currentActionOffset = 0;
    private int _attacksLeft = 0;
    private int _rotateDirection = 1;
    protected override void Start()
    {
        base.Start();
        OnDeath += OnDie;
        _currentPatrollingPoint = GetPatrollingPoint();
    }

    protected override void Update()
    {
        base.Update();
        if(target is null)
        {
            OnPatrolling();
        }
        else
        {
            switch(fightingActionState)
            {
                case FightingActionState.Attacking:
                    OnAttackingPlayer();
                    break;
                case FightingActionState.Blocking:
                    OnBlocking();
                    break;
                case FightingActionState.RotatingAround:
                    OnRotatingAroundPlayer();
                    break;
                case FightingActionState.Watching:
                    OnWatchingPlayer();
                    break;
                default:
                    OnPursuingPlayer();
                    break;
            }
        }
    }

    private void OnPatrolling()
    {
        if(Vector3.Distance(transform.position, _currentPatrollingPoint) <= 0.3f)
        {
            _currentActionOffset -= Time.deltaTime;
            if(_currentActionOffset <= 0)
            {
                _currentActionOffset = Random.Range(MIN_ACTION_TIME, MAX_ACTION_TIME);
                _currentPatrollingPoint = GetPatrollingPoint();
            }
        }
        else
        {
            OnWalkingTowards(_currentPatrollingPoint);
        }
    }
    private void OnWalkingTowards(Vector3 point)
    {
        var direction = point - transform.position;
        direction.y = transform.position.y;
        direction.Normalize();
        var localizedDirection = transform.InverseTransformDirection(direction);
        OnRobotMove(localizedDirection.z, localizedDirection.x);
    }
    private void OnPursuingPlayer()
    {
        var playerFlatPosition = new Vector3(
            target.transform.position.x,
            transform.position.y,
            target.transform.position.z
            );
        var distanceToPlayer = Vector3.Distance(transform.position, playerFlatPosition);
        if(distanceToPlayer >= ACTION_TRIGGER_DISTANCE)
        {
            OnWalkingTowards(target.transform.position);
        }
        else
        {
            OnRobotMove(0, 0);
            ChooseFightingAction();
        }
    }

    private void ChooseFightingAction()
    {
        float dice = Random.Range(0, 10f);
        _currentActionOffset = Random.Range(MIN_ACTION_TIME, MAX_ACTION_TIME);
        if (dice <= ATTACK_PROBABILITY_RANGE)
        {
            _currentActionOffset = TIME_BETWEEN_ATTACKS;
            _attacksLeft = Random.Range(1, MAX_ATTACKS_PER_ACTION + 1);
            fightingActionState = FightingActionState.Attacking;
        }
        else if(dice <= BLOCK_PROBABILITY_RANGE)
        {
            ToggleBlocking(true);
            fightingActionState = FightingActionState.Blocking;
        }
        else if(dice <= ROTATE_AROUND_PROBABILITY_RANGE)
        {
            int[] direction = { 1, -1 };
            _rotateDirection = direction[Random.Range(0, direction.Length)];
            fightingActionState = FightingActionState.RotatingAround;
        }
        else
        {
            fightingActionState = FightingActionState.Watching;
        }
    }

    private void OnRotatingAroundPlayer()
    {
        _currentActionOffset -= Time.deltaTime;
        if(_currentActionOffset >= 0)
        {
            OnRobotMove(0, _rotateDirection);
        }
        else
        {
            fightingActionState = FightingActionState.Pursuing;
        }
    }

    private void OnAttackingPlayer()
    {
        if(_attacksLeft != 0)
        {
            _currentActionOffset -= Time.deltaTime;
            if(_currentActionOffset <= 0)
            {
                _attacksLeft--;
                OnEnemyAttack();
            }
        }
        else
        {
            fightingActionState = FightingActionState.Pursuing;
        }
    }

    private void OnEnemyAttack()
    {
        OnRobotAttack();
        bool successfullHit = Physics.Raycast(
            transform.position + Vector3.up,
            transform.forward,
            out RaycastHit hitInfo,
            HIT_DISTANCE
            );

        if (successfullHit && hitInfo.transform.gameObject.TryGetComponent<CombatantPlayer>(out CombatantPlayer player))
        {
            player.OnReceiveDamage(HIT_BASE_POWER * lightAttackIndex);
        }
    }

    private void OnBlocking()
    {
        _currentActionOffset -= Time.deltaTime;
        if (_currentActionOffset <= 0)
        {
            ToggleBlocking(false);
            fightingActionState = FightingActionState.Pursuing;
        }
    }

    private void OnWatchingPlayer()
    {
        _currentActionOffset -= Time.deltaTime;
        if (_currentActionOffset <= 0)
        {
            fightingActionState = FightingActionState.Pursuing;
        }
    }

    private void OnDie()
    {
        var deadInstance = deadAnimationPrefabs.prefabs[Random.Range(0, deadAnimationPrefabs.prefabs.Length)];
        Instantiate(deadInstance, transform.position, transform.rotation);
        Destroy(gameObject);
    }

    private Vector3 GetPatrollingPoint()
    {
        return patrollingCheckpoints[Random.Range(0, patrollingCheckpoints.Length)];
    }
}

enum FightingActionState
{
    Pursuing,
    Attacking,
    Blocking,
    RotatingAround,
    Watching
}
