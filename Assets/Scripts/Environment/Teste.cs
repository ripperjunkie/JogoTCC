using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teste : MonoBehaviour
{
private GameObject _player = null;
private bool bOnCollsion;
private Rigidbody _rBBox = null;
public float force = 0;
private float _speedPlayer = 0;
private float _speedInitial = 0;
private Vector3 _forceD;
public bool _active = false;
private FixedJoint _fixedJ;
private Animator _animator;
private AudioSource _audioSource;
private float _lerpRigidSpeed = 1f;
private float _groundLocoSpeed;

   void Start()
   {
     _rBBox = gameObject.GetComponent<Rigidbody>();
     _player =  GameObject.Find("PlayerMaster");    
     _speedInitial = _player.GetComponent<CharacterMovement>().movSpeed;
     _animator = _player.GetComponent<Animator>();
     _fixedJ = GetComponent<FixedJoint>();
     _audioSource = GetComponent<AudioSource>();
   }

  void OnTriggerEnter(Collider col)
  {
       if(col.gameObject.CompareTag("Player"))
      {      
        bOnCollsion = true;
       }
  }
    void OnTriggerExit(Collider col)
  {
       if(col.gameObject.CompareTag("Player"))
      {
        bOnCollsion = false;
       }
  }

  void FixedUpdate()
    {
       _forceD = new Vector3(Input.GetAxis("Horizontal"), 0 ,Input.GetAxis("Vertical"));

        if(Input.GetKeyDown(KeyCode.E) && bOnCollsion && !_active)
        {        
           _active = true;
           _animator.SetBool( "move_Objects", true);
        }
         else if(Input.GetKeyDown(KeyCode.E) && _active)
        {        
           _active = false;
           _player.GetComponent<CharacterMovement>().movSpeed = _speedInitial;
            _animator.SetBool( "move_Objects", false);
        }

         Move();       
    }

    void Move()
    {
      if(_active){

        _player.GetComponent<CharacterMovement>().movSpeed = _speedPlayer;
        _rBBox.WakeUp();
        _forceD.y = 0;
        _forceD.Normalize();
        _player.transform.LookAt(transform);
        _fixedJ.connectedBody = _player.GetComponent<Rigidbody>();
         _rBBox.AddForce(_forceD * force, ForceMode.Impulse);
       
        _groundLocoSpeed = Mathf.Lerp(_animator.GetFloat("move_object"), _rBBox.velocity.magnitude, _lerpRigidSpeed);
        _animator.SetFloat("move_object", _groundLocoSpeed);
        if(_groundLocoSpeed >= 1)
        {
          _audioSource.Play();
        }
        else
        {
          _audioSource.Stop();
        }
      }
      else 
      {
        _rBBox.Sleep();
        _player.transform.LookAt(null);
        _fixedJ.connectedBody = null;
      }   
    }
}
