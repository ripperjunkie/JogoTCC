using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public EMovementStateEnemy movementState;

    [SerializeField] private bool _isDebug;

    private CharacterController _charController;




    private void Start()
    {
        
    }

    private void Update()
    {

    }


    public void ResetMovementState()
    {
        movementState = EMovementStateEnemy.NONE;
    }

    public void SetSwinging()
    {
        movementState = EMovementStateEnemy.PATROL;
    }

    public void SetIdle()
    {
        movementState = EMovementStateEnemy.IDLE;
    }

    public void SetPatrol()
    {
        movementState = EMovementStateEnemy.PATROL;
    }

    public void SetChasing()
    {
        movementState = EMovementStateEnemy.CHASING;
    }

    public void SetRiding()
    {
        movementState = EMovementStateEnemy.ATTACKING;

    }
}
