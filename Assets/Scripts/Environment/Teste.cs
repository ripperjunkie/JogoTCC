using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teste : MonoBehaviour
{
 private bool bOnMove = false;
 private  bool bOnCollsion = false;
 private Transform _arms = null;
private Animator _animator;
private BoxCollider _boxCollider;
private Rigidbody  _rdBody;
private Vector3 initialPosition;

 void Start()
 {

   initialPosition = transform.position;
 }
  
  void OnTriggerEnter(Collider col)
  {

      if(col.gameObject.CompareTag("Respawn"))
      {
       transform.position = initialPosition;    
      }     
  }

}
