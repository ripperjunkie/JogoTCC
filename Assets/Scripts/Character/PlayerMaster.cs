using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMaster : MonoBehaviour
{
    public EMovementState movementState;

    [SerializeField] private bool _isDebug;
    private Rigidbody _rb;
    private CharacterMovement _charMovement;
    

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _charMovement = GetComponent<CharacterMovement>();
    }

    private void Update()
    {
        if(_rb)
        {
            if(movementState != EMovementState.SWINGING && movementState != EMovementState.CLIMBING && movementState != EMovementState.CROUCHING)
            {
                if (_rb.velocity.magnitude != 0f && _charMovement.HitGround())
                {
                    movementState = EMovementState.WALKING;
                }
                if (_rb.velocity.y != 0f && !_charMovement.HitGround())
                {
                    movementState = EMovementState.INAIR;
                }
            } 
        }

        if(_isDebug)
        {
            print("Movement state: " + movementState);
        }        
    }

    public void ResetMovementState()
    {
        movementState = EMovementState.NONE;
    }

    public void SetSwinging()
    {
        movementState = EMovementState.SWINGING;
    }

    public void SetCrouching()
    {
        movementState = EMovementState.CROUCHING;
    }

    public void SetClimbing()
    {
        movementState = EMovementState.CLIMBING;
    }


}
