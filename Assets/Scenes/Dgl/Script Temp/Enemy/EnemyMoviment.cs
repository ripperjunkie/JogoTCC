using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(EnemyManager))]

public class EnemyMoviment : MonoBehaviour
{
    private EnemyManager enemyManager;

    public float minDistance;
    public float maxDistanceToChasing;

    private float distanciaPlayer;

    private GameObject player;
    private NavMeshAgent navMesh;

    //patroll
    public Transform[] walkPoints;
    private int toGo;
    public string nameAnimationWalk, nameAnimationIdle;
    public float idleTime;
    private bool walkPointSet = true;


    private Animator _animator;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        navMesh = GetComponent<NavMeshAgent>();
        enemyManager = GetComponent<EnemyManager>();
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        if(enemyManager.movementState == EMovementStateEnemy.CHASING)
        {
            Chasing();
            return;
        }
        Patroling();

    }

    private void Chasing()
    {
        distanciaPlayer = Vector3.Distance(player.transform.position, transform.position);
        if (distanciaPlayer < minDistance)
        {

        }
        if (distanciaPlayer < maxDistanceToChasing)
        {
            
            navMesh.destination = player.transform.position;
            _animator.SetFloat("ground_mov_speed", 5);
        }
        else
        {
            StopChasing();
        }
    }

    private void StopChasing()
    {
        enemyManager.movementState = EMovementStateEnemy.PATROL;
        walkPointSet = true;
        navMesh.destination = transform.position;
        _animator.SetFloat("ground_mov_speed", 0);
    }

    private void Patroling()
    {
        if (walkPointSet)
        {
            navMesh.SetDestination(walkPoints[toGo].position);
            enemyManager.movementState = EMovementStateEnemy.PATROL;
            //_animator.Play(nameAnimationWalk);
            walkPointSet = false;
        }


        Vector3 distanceToWalkPoint = transform.position - walkPoints[toGo].position;

        //chegou ao ponto
        if (distanceToWalkPoint.magnitude < 1f && enemyManager.movementState != EMovementStateEnemy.IDLE)
        {
            //_animator.Play(nameAnimationIdle);
            walkPointSet = false;
            Invoke("NextPoint", idleTime);
            enemyManager.movementState = EMovementStateEnemy.IDLE;
        }
    }
    void NextPoint()
    {
        if (toGo+1 < walkPoints.Length)
        {
            toGo++;
        }
        else
        {
            toGo = 0;
        }
        walkPointSet = true;
    }


}
