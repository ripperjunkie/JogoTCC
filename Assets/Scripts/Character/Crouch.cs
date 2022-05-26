using UnityEngine;

public class Crouch : MonoBehaviour
{
    [SerializeField] private float _crouchCapsuleHeight = 1.29f;
    [SerializeField] private float _crouchSpeed;
    
    private float _defaultCapsuleHeight;
    private CapsuleCollider _capsuleCollider;
    private CharacterMovement _charMovement;
    private PlayerMaster _playerMaster;
    private float _defaultSpeed;

    private void Awake()
    {
        _playerMaster = GetComponent<PlayerMaster>();
        _defaultCapsuleHeight = GetComponent<CapsuleCollider>().height;
        _capsuleCollider = GetComponent<CapsuleCollider>();
        _charMovement = GetComponent<CharacterMovement>();
        _defaultSpeed = _charMovement.currentSpeed;
        if(_playerMaster && _playerMaster.characterData)
        {
            _crouchSpeed = _playerMaster.characterData.crouchSpeed;
        }
    }

    private void Update()
    {
        InputCrouch();
    }

    public void InputCrouch()
    {
        bool canCrouch = _playerMaster.movementState != EMovementState.CROUCHING && _playerMaster.movementState != EMovementState.INAIR;

        if (Input.GetKeyDown(KeyCode.LeftControl) && canCrouch)
        {
            StartCrouch();
        }
        else if(Input.GetKeyDown(KeyCode.LeftControl) && _playerMaster.movementState == EMovementState.CROUCHING)
        {
            StopCrouch();
        }
    }

    public void StartCrouch()
    {
        _playerMaster.movementState = EMovementState.CROUCHING;
        _capsuleCollider.height = _crouchCapsuleHeight;
        _charMovement.currentSpeed = _crouchSpeed;
    }

    public void StopCrouch()
    {
        _playerMaster.movementState = EMovementState.NONE;
        _capsuleCollider.height = _defaultCapsuleHeight;
        _charMovement.currentSpeed = _playerMaster.characterData.jogSpeed;
        print("StopCrouch");

    }
}
