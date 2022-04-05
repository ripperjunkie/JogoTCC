using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObjects : MonoBehaviour
{
   private CharacterMovement _player = null;
   private bool bOnCollsion;
   private Rigidbody _rBBox = null;
   public float force = 0;
   public float SpeedPlayer = 0;
   private float SpeedInitial = 0;
   private Vector3 _forceD;
   private bool _active = false;

   void Start()
   {
     _player = GetComponentInParent<CharacterMovement>();
     SpeedInitial = _player.movSpeed;
   }

  void OnTriggerEnter(Collider col)
  {
       if(col.gameObject.CompareTag("BoxMove"))
      {
        _rBBox = col.gameObject.GetComponent<Rigidbody>();
        bOnCollsion = true;
       }
  }
    void OnTriggerExit(Collider col)
  {
       if(col.gameObject.CompareTag("BoxMove"))
      {
        bOnCollsion = false;
       }
  }

  void FixedUpdate()
    {
       _forceD = new Vector3(Input.GetAxis("Horizontal"), 0 ,Input.GetAxis("Vertical"));

        if(Input.GetKey(KeyCode.E) && bOnCollsion && !_active)
        {        
           _active = true;
        }
         else if(Input.GetKey(KeyCode.E) && _active)
        {        
           _active = false;
           _player.movSpeed = SpeedInitial;
           _rBBox = null;
        }

         Move();              
    }

    void Move()
    {
      if(_active && _rBBox){
        _player.movSpeed = SpeedPlayer;
        _forceD.y = 0;
        _forceD.Normalize();
        _rBBox.AddForce(_forceD * force, ForceMode.Impulse);   
      }   
    }
}
