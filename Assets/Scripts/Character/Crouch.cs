using UnityEngine;

public class Crouch : MonoBehaviour
{
    private PlayerMaster _playerMaster;
    [SerializeField] private float _crouchCapsuleHeight = 1.29f;
    
    [SerializeField] private float _defaultCapsuleHeight;
    private CapsuleCollider _capsuleCollider;

    private void Awake()
    {
        _defaultCapsuleHeight = GetComponent<CapsuleCollider>().height;
        _capsuleCollider = GetComponent<CapsuleCollider>();
    }

    private void Start()
    {
        _playerMaster = GetComponent<PlayerMaster>();
    }

    private void Update()
    {
        InputCrouch();
    }

    public void InputCrouch()
    {
        bool canCrouch = _playerMaster.movementState != EMovementState.CROUCHING && _playerMaster.movementState != EMovementState.INAIR;

        if (Input.GetKeyDown(KeyCode.C) && canCrouch)
        {
            StartCrouch();
        }
        else if(Input.GetKeyDown(KeyCode.C) && _playerMaster.movementState == EMovementState.CROUCHING)
        {
            StopCrouch();
        }
    }

    public void StartCrouch()
    {
        _playerMaster.movementState = EMovementState.CROUCHING;
        _capsuleCollider.height = _crouchCapsuleHeight;
    }

    public void StopCrouch()
    {
        _playerMaster.movementState = EMovementState.NONE;
        _capsuleCollider.height = _defaultCapsuleHeight;

    }
}
