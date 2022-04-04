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
    private PlayerMaster _playerMasterRef;


    private void Start()
    {
        _playerMasterRef = GetComponent<PlayerMaster>();

    }


    private void Update()
    {
        GroundLocomotion();      
        if(_animator && _playerMasterRef)
        {
            _animator.SetInteger("anim_state", (int)_playerMasterRef.movementState);
            _animator.SetLayerWeight(1, _ = _playerMasterRef.GetIsYoyoActive ? 1f : 0f);
        }
        if (_rigidbody)
        {
            _animator.SetFloat("vertical_speed", _rigidbody.velocity.y);
        }

        //print("Yoyo active?" + _playerMasterRef.GetIsYoyoActive);
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
