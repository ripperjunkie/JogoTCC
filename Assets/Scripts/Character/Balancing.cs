using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balancing : MonoBehaviour
{
    private PlayerMaster _playerMaster;

    private CharacterMovement _charMovement;
    private float _defaultSpeed;
    [SerializeField] private float _balanceSpeed;


    private void Awake()
    {
        _charMovement = GetComponent<CharacterMovement>();
        _defaultSpeed = _charMovement.movSpeed;
    }

    void Start()
    {
        _playerMaster = GetComponent<PlayerMaster>();        
    }

    private void Update()
    {
        InputBalancing();
    }

    public void InputBalancing()
    {
        bool canBalancing = _playerMaster.movementState != EMovementState.BALANCING && _playerMaster.movementState != EMovementState.INAIR;

        if (_charMovement.HitBrige() && canBalancing)
        {
            StartBalance();
        }
        else if (!_charMovement.HitBrige() && _playerMaster.movementState == EMovementState.BALANCING)
        {
            StopBalance();
        }
    }

    public void StartBalance()
    {
        _playerMaster.movementState = EMovementState.BALANCING;
        _charMovement.movSpeed = _balanceSpeed;
    }

    public void StopBalance()
    {
        _playerMaster.movementState = EMovementState.NONE;
        _charMovement.movSpeed = _defaultSpeed;

    }
}
