using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpObjects : MonoBehaviour
{
 private bool bOnMove = false;
 private  bool bOnCollsion = false;
 private Transform _arms = null;
 private GameObject _objectMove = null;
private Animator _animator;
private BoxCollider _boxCollider;
private Rigidbody _rBBox;
public Transform initialPosition = null;

 void Start()
 {
   _arms = GameObject.Find("PickUP").GetComponent<Transform>();
   _animator = GetComponentInParent<Animator>();
 }
  
  void OnTriggerEnter(Collider col)
  {
      if(col.gameObject.CompareTag("Box") && !_objectMove)
      {
        bOnCollsion = true;
        _objectMove = col.gameObject;
        _boxCollider = col.gameObject.GetComponent<BoxCollider>();
        _rBBox = col.gameObject.GetComponent<Rigidbody>();
        initialPosition = col.transform;
      }    
  }
    void OnTriggerExit(Collider col)
  {
      if(col.gameObject.CompareTag("Box") && !_objectMove)
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
        else if(Input.GetKeyDown(KeyCode.E) && bOnMove && bOnCollsion)
        {
          DropObject();
        }
    }
    IEnumerator MovingObject()
    {  
      if(_objectMove)
      {
          bOnMove = true;
          _animator.SetTrigger("pick_up");
           yield return new WaitForSeconds(0.6f);
          _boxCollider.enabled = false;
         _objectMove.transform.position = _arms.transform.position;
         _objectMove.transform.SetParent(_arms);
      }
    }
    void DropObject()
    {
        StopCoroutine(MovingObject());
        _objectMove.transform.SetParent(null);
        _boxCollider.enabled = true;
        _boxCollider = null;
        _objectMove = null;
        bOnMove = false;
        bOnCollsion = false;
    }
}
