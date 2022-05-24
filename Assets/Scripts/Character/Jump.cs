using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{
    [SerializeField] private float _jumpForce = 20f;
    private CharacterMovement _charMovement;
    private Rigidbody _rigidbody;
    private PlayerMaster _playerMaster;

    private void Awake()
    {
        _charMovement = GetComponent<CharacterMovement>();
        _rigidbody = GetComponent<Rigidbody>();
        _playerMaster = GetComponent<PlayerMaster>();
        if (_playerMaster)
        {
            _jumpForce = _playerMaster.characterData.jumpHeight;
        }
    }

    private void Update()
    {
        ApplyJump();
        
    }

    public void ApplyJump()
    {
        if (Input.GetButtonDown("Jump") && _charMovement.HitGround())
        {
             _rigidbody.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
        }
    }
}
