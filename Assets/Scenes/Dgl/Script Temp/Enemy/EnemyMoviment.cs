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

    public bool activeForDistance;
    public bool activeForCollider;

    public float minDistance;
    public float maxDistanceToChasing;

    private float distanciaPlayer;

    private GameObject player;
    private NavMeshAgent navMesh;



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
        distanciaPlayer = Vector3.Distance(player.transform.position, transform.position);
        Debug.Log("Distancia Player:" + distanciaPlayer);
        if (activeForDistance && distanciaPlayer < maxDistanceToChasing)
        {
            StartChasing();
            Debug.Log("perseguindo Player:");

        }
        if (distanciaPlayer <= minDistance || distanciaPlayer > maxDistanceToChasing)
        {
            StopChasing();
            Debug.Log("perseguindo Player:");
        }
    }

    private void OnTriggerEnter(Collider objectCollider)
    {
        //if (objectCollider.tag == "Player" && activeForCollider)
        //{
        //    navMesh.destination = player.transform.position;
        //}

            if (objectCollider.gameObject.CompareTag("Player"))
            {
                FindObjectOfType<CanvasFadeAnimation>().PlayFadeOut();
            }
    }

    private void StartChasing()
    {
        enemyManager.movementState = EMovementStateEnemy.CHASING;
        navMesh.destination = player.transform.position;
        _animator.SetFloat("ground_mov_speed", 5);
    }
    private void StopChasing()
    {
        enemyManager.movementState = EMovementStateEnemy.IDLE;
        navMesh.destination = transform.position;
        _animator.SetFloat("ground_mov_speed", 0);

    }

}
