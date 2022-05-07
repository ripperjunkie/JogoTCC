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
   _arms = GameObject.Find("PickUP").GetComponent<Transform>();
   _animator = _arms.GetComponentInParent<Animator>();
  _boxCollider = GetComponent<BoxCollider>();
   _rdBody = GetComponent<Rigidbody>();
   initialPosition = transform.position;
 }
  
  void OnTriggerEnter(Collider col)
  {
      if(col.gameObject.CompareTag("Player"))
      {
        bOnCollsion = true;     
      }

      if(col.gameObject.CompareTag("BoxMove"))
      {
       transform.position = initialPosition;    
      }     
  }
    void OnTriggerExit(Collider col)
  {
      if(col.gameObject.CompareTag("Player"))
      {
        bOnCollsion = false;
      }    
  }
     void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) && !bOnMove && bOnCollsion)
        {  
         
           StartCoroutine(MovingObject());  
        }
        else if(Input.GetKeyDown(KeyCode.E) && bOnMove)
        {
          DropObject();
        }
    }
    IEnumerator MovingObject()
    {  
      bOnMove = true;
      _animator.SetTrigger("pick_up");
      yield return new WaitForSeconds(0.6f);

       _boxCollider.isTrigger = true;
       _rdBody.isKinematic = true;
        transform.position = _arms.transform.position;
        transform.SetParent(_arms);
    }
    void DropObject()
    {
      StopCoroutine(MovingObject());
      transform.SetParent(null);
      _boxCollider.isTrigger = false;
      _rdBody.isKinematic = false;
      bOnMove = false;
      bOnCollsion = false;
    }
}
