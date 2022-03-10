using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMaster : MonoBehaviour
{   
    private Rigidbody _rb;
    private CharacterMovement _charMovement;
    private EMovementState _movementState;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _charMovement = GetComponent<CharacterMovement>();
    }

    public EMovementState GetMovementState()
    {
        return _movementState;
    }

    private void Update()
    {
        if(_rb)
        {
            if(_movementState != EMovementState.SWINGING && _movementState != EMovementState.CLIMBING)
            {
                if (_rb.velocity.magnitude != 0f && _charMovement.HitGround())
                {
                    _movementState = EMovementState.WALKING;
                }
                if (_rb.velocity.y != 0f && !_charMovement.HitGround())
                {
                    _movementState = EMovementState.INAIR;
                }
            }
 
        }

        print("Movement state: " + _movementState);
    }

    public void ResetMovementState()
    {
        _movementState = EMovementState.NONE;
    }

    public void SetSwinging()
    {
        _movementState = EMovementState.SWINGING;
    }

    public void SetCrouching()
    {
        _movementState = EMovementState.CROUCHING;
    }

    public void SetClimbing()
    {
        _movementState = EMovementState.CLIMBING;
    }


}
