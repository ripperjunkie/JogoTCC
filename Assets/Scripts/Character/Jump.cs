using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{
    private CharacterMovement _charMovement;
    private Rigidbody _rigidbody;
    [SerializeField] private float _jumpForce = 20f;
    void Start()
    {
        _charMovement = GetComponent<CharacterMovement>();
        _rigidbody = GetComponent<Rigidbody>();
    }
    void Update()
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
