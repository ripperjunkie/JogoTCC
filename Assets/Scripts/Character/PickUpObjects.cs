using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpObjects : MonoBehaviour
{
 private bool bOnMove = false;
 private  bool bOnCollsion = false;
 private Transform _arms = null;
 public GameObject _objectMove = null;
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
      if(col.gameObject.CompareTag("Box") && !bOnMove)
      {
        bOnCollsion = false;
        _objectMove = null;
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
      if(_objectMove)
      {
          bOnMove = true;
  
           yield return new WaitForSeconds(0.6f);
           _rBBox.isKinematic = true;
          _boxCollider.enabled = false;
         _objectMove.transform.position = _arms.transform.position;
         _objectMove.transform.SetParent(_arms);
      }
    }
    void DropObject()
    {
        StopCoroutine(MovingObject());
         bOnMove = false;
         bOnCollsion = false;
        _objectMove.transform.SetParent(null);
        _boxCollider.enabled = true;
        _rBBox.isKinematic = false;
        _boxCollider = null;
        _objectMove = null;           
    }
}
