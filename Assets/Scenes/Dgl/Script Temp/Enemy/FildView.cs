using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FildView : MonoBehaviour
{
    private float viewRadius;
    //[Range (0, 360)]
    private float viewAngle = 45;

    private EnemyMoviment enemyMoviment;
    private EnemyManager enemyManager;


    public Vector3 DilFromAngle(float angleInDegrees, bool angleIsGlobal)
    {
        if (!angleIsGlobal)
        {
            angleInDegrees += transform.eulerAngles.y;
        }
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }

    public LayerMask targetMask;
    public LayerMask obstacleMask;

    [HideInInspector]
    public List<Transform> visibleTargets = new List<Transform>();

    private void Start()
    {
        enemyMoviment = GetComponent<EnemyMoviment>();
        enemyManager = GetComponent<EnemyManager>();
        viewRadius = enemyMoviment.maxDistanceToChasing;
        StartCoroutine("FindTargetsWithDelay", 1f);
    }

    IEnumerator FindTargetsWithDelay(float delay)
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
            FindVisibleTargets();
        }
    }

    void FindVisibleTargets()
    {
        visibleTargets.Clear();
        Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, viewRadius, targetMask);

        for (int i = 0; i < targetsInViewRadius.Length; i++)
        {
            Transform target = targetsInViewRadius[i].transform;
            Vector3 dirToTarget = (target.position - transform.position).normalized;
            if (Vector3.Angle(transform.forward, dirToTarget) < viewAngle / 2)
            {
                float dstToTarget = Vector3.Distance(transform.position, target.position);
                if (!Physics.Raycast(transform.position, dirToTarget, dstToTarget, obstacleMask))
                {
                    visibleTargets.Add(target);
                    // colocar açoes para realizar no alvo aqui!!!!!
                    enemyManager.movementState = EMovementStateEnemy.CHASING;
                }
            }
        }
    }
}