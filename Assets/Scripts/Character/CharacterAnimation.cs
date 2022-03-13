using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class CharacterAnimation : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private float _currentSpeed;

    [SerializeField] private float _lerpRigidSpeed = 3f;
    private float _groundLocoSpeed = 0f;
    private PlayerMaster _playerMaster; 

    private void Start()
    {
        _playerMaster = GetComponent<PlayerMaster>();
    }

    private void Update()
    {
        GroundLocomotion();      
        if(_animator && _playerMaster)
        {
            _animator.SetInteger("anim_state", (int)_playerMaster.movementState);
        }
    }

    private void GroundLocomotion()
    {

        _groundLocoSpeed = Mathf.Lerp(_animator.GetFloat("ground_mov_speed"), _rigidbody.velocity.magnitude, _lerpRigidSpeed);
        //get character speed
        if (_rigidbody)
        {
            _animator.SetFloat("ground_mov_speed", _groundLocoSpeed);

        }
        //print(_rigidbody.velocity.magnitude);

    }
}
