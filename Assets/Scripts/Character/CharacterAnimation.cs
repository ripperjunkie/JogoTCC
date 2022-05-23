using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class CharacterAnimation : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private Rigidbody _rigidbody;

    [SerializeField] private float _lerpRigidSpeed = 3f;
    private float _groundLocoSpeed = 0f;
    private PlayerMaster _playerMasterRef;
    private CharacterMovement _charMovement;


    private void Start()
    {
        _playerMasterRef = GetComponentInParent<PlayerMaster>();
        _charMovement = GetComponentInParent<CharacterMovement>();  

    }



    private void Update()
    {
        StartCoroutine(CalculateSpeed());
        GroundLocomotion();      
        if(_animator && _playerMasterRef)
        {
            _animator.SetInteger("anim_state", (int)_playerMasterRef.movementState);
            //_animator.SetLayerWeight(1, _ = _playerMasterRef.GetIsYoyoActive ? 1f : 0f);
        }
        if (_rigidbody)
        {
            _animator.SetFloat("vertical_speed", _rigidbody.velocity.y);
        }

        //print("Yoyo active?" + _playerMasterRef.GetIsYoyoActive);
    }

    private void GroundLocomotion()
    {     
        //get character speed
        if (_rigidbody)
        {
            _animator.SetFloat("ground_mov_speed", _groundLocoSpeed, _lerpRigidSpeed, Time.deltaTime);
        }
        print("currentSpeed: " + _groundLocoSpeed);

    }

    public IEnumerator CalculateSpeed()
    {
        Vector3 lasPos = transform.position;
        yield return new WaitForFixedUpdate();
        _groundLocoSpeed = (lasPos - transform.position).magnitude / Time.deltaTime;

    }


}
