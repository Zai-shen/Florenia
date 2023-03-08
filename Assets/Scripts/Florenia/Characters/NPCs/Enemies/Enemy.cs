using System;
using System.Collections;
using System.Collections.Generic;
using Florenia.Managers;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Attack))]
public class Enemy : MonoBehaviour
{
    public LayerMask IgnoreSightCheck;

    #region Patroling

    public bool UsePatroling = true;
    public bool UseAggroRange = true;
    public float AggroRange = 3f;
    public Vector2 WalkPoint;
    private bool _walkPointSet;
    public float WalkPointRange = 2f;
    
    #endregion

    // #region Health
    //
    // public Health Health;
    //
    // #endregion
    
    #region Attacking
    
    private Attack _attack;
    
    #endregion

    #region States

    public bool PlayerInAttackRange;

    #endregion

    #region Navigation

    public float SearchCooldown = 0.1f;
    private float _searchCooldown;
    public float MoveSpeed = 2f;
    private Transform _target;
    private NavMeshAgent _agent;
    private NavMeshPath _navMeshPath;
    
    #endregion

    // #region Animation
    //
    // protected Animator _animator;
    //
    // #endregion
    
    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _agent.speed = MoveSpeed;
        _navMeshPath = new NavMeshPath();
        // _animator = GetComponent<Animator>();
        
        _attack = GetComponent<Attack>();
        // _attack.EAnimator = _animator;
    }
    

    private void OnEnable()
    {
        PlayerManager.PlayerSpawn += SetTarget;
        Globals.Enemies.Add(this.gameObject);
    }
    
    private void OnDisable()
    {
        PlayerManager.PlayerSpawn -= SetTarget;
        Globals.Enemies.Remove(this.gameObject);
    }

    private void SetTarget()
    {
        _target = PlayerManager.Instance.InGamePlayer.transform;
        _attack.Target = _target;
    }
    
    private void Update()
    {
        PlayerInAttackRange = _attack.CheckTargetInAttackRange();

        if (UseAggroRange)
        {
            if (UsePatroling && !PlayerInAttackRange) Patrole();
            float _distanceToPlayer = _attack.DistanceToTarget();
            if (_distanceToPlayer <= AggroRange)
            {
                if (!PlayerInAttackRange && !_attack.OnCooldown) ChasePlayer();
                if (PlayerInAttackRange) Attack();
            }
        }
    }

    private void Attack()
    {
        _agent.SetDestination(transform.position);
        // _animator.SetFloat("MovSpeed", 0f);
        FaceTarget();
        
        _attack.DoStartAttack();
    }

    private void ChasePlayer()
    {
        if (_agent.CalculatePath(_target.position, _navMeshPath))
        {
            _agent.SetDestination(_target.position);
            // _animator.SetFloat("MovSpeed", 1f);
        }
        else
        {
            _agent.SetDestination(transform.position);
            // _animator.SetFloat("MovSpeed", 0f);
        }
    }

    [ContextMenu("Patrole")]
    private void Patrole()
    {
        if (!_walkPointSet) SearchWalkPoint();

        if (_walkPointSet) _agent.SetDestination(WalkPoint);

        if (_agent.remainingDistance <= _agent.stoppingDistance)
        {
            _walkPointSet = false;
        }
    }

    private void SearchWalkPoint()
    {
        float _randomX = Random.Range(-WalkPointRange, WalkPointRange);
        float _randomY = Random.Range(-WalkPointRange, WalkPointRange);
        Vector2 _difference = new(_randomX, _randomY);

        WalkPoint = (Vector2) transform.position + _difference;
        
        if (_agent.CalculatePath(WalkPoint, _navMeshPath))
        {
            _walkPointSet = true;
        }else if (PlayerInAttackRange)
        {
            _walkPointSet = true;
        }
    }

    private void FaceTarget()
    {
        // Vector2 direction = _target.transform.position - transform.position;
        // direction.Normalize();
        // float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        // transform.rotation = Quaternion.Euler(Vector3.forward * angle);
    }
    
    void Die()
    {
        Destroy(this.gameObject);
    }
    
    private void OnDrawGizmosSelected()
    {
        if (UseAggroRange)
        {
            Gizmos.color = Color.magenta;
            Gizmos.DrawWireSphere(transform.position, AggroRange);
        }

        if (UsePatroling)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(transform.position, WalkPoint);
        }
        
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, _target ? _target.position : Vector3.zero );
    }
}


// public class Enemy : MonoBehaviour
// {
//     [SerializeField] private float aggroRadius;
//     private GameObject player;
//     private float speed;
//     private float distance;
//     
//     private void Update()
//     {
//         distance = Vector2.Distance(transform.position, player.transform.position);
//         Vector2 direction = player.transform.position - transform.position;
//         direction.Normalize();
//         float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
//
//
//         if (distance <= aggroRadius)
//         {
//             transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
//             transform.rotation = Quaternion.Euler(Vector3.forward * angle);
//         }
//     }
//
// }
