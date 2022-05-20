using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sprint : MonoBehaviour
{
    private PlayerMaster _playerMaster;
    [SerializeField] private float _sprintSpeed;

    private float _defaultCapsuleHeight;
    private CapsuleCollider _capsuleCollider;
    private CharacterMovement _charMovement;
    private float _defaultSpeed;

    private void Awake()
    {
        _defaultCapsuleHeight = GetComponent<CapsuleCollider>().height;
        _capsuleCollider = GetComponent<CapsuleCollider>();
        _charMovement = GetComponent<CharacterMovement>();
        _defaultSpeed = _charMovement.currentSpeed;
    }

    private void Start()
    {
        _playerMaster = GetComponent<PlayerMaster>();
    }

    private void Update()
    {
        InputSprint();
    }

    public void InputSprint()
    {
        bool canSprint =  _playerMaster.movementState != EMovementState.INAIR && _playerMaster.movementState != EMovementState.CROUCHING;

        if (Input.GetButtonDown("Sprint") && canSprint)
        {
            StartSprint();
        }
        else if (Input.GetButtonUp("Sprint") && _playerMaster.movementState == EMovementState.SPRINTING)
        {
            StopSprint();
        }
    }

    public void StartSprint()
    {
        _playerMaster.movementState = EMovementState.SPRINTING;
        _charMovement.currentSpeed = _sprintSpeed;
        print("StartSprint()");
    }

    public void StopSprint()
    {
        _playerMaster.movementState = EMovementState.NONE;
        _charMovement.currentSpeed = _charMovement.initialDefaultSpeed;
        print("StopSprint");

    }
}
