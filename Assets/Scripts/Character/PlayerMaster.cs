using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMaster : MonoBehaviour
{
    public EMovementState movementState;

    [SerializeField] private bool _isDebug;
    private Rigidbody _rb;
    private CharacterMovement _charMovement;

    private bool _isYoyoActive;
    private bool _canEquipYoyo = true;
    public bool GetIsYoyoActive { get => _isYoyoActive; }



    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _charMovement = GetComponent<CharacterMovement>();
    }

    private void Update()
    {
        if(_rb)
        {
            if(movementState != EMovementState.SWINGING && movementState != EMovementState.CLIMBING && 
                movementState != EMovementState.CROUCHING && movementState != EMovementState.BALANCING && 
                movementState != EMovementState.RIDING && movementState != EMovementState.RAPPEL)
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
        ToggleEquipYoyo();

        if(_isDebug)
        {
            print("Movement state: " + movementState);
            print("Yoyo active: " + GetIsYoyoActive);
        }
    }

    public void ToggleEquipYoyo()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1) && !_isYoyoActive)
        {
            EquipYoyo();
        }
        if(Input.GetKeyUp(KeyCode.Mouse1) && _isYoyoActive)
        {
            UnequipYoyo();
        }
    }

    public void EquipYoyo()
    {
        if(_canEquipYoyo)
        {
            _isYoyoActive = true;
        }
    }
    public void UnequipYoyo()
    {
        _isYoyoActive = false;
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

    public void SetBalacing()
    {
        movementState = EMovementState.BALANCING;
    }

    public void SetRiding()
    {
        movementState = EMovementState.RIDING;
    }
}
