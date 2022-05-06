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
           bOnMove = true;
           _animator.SetTrigger("pick_up");            
        }
        else if(Input.GetKeyDown(KeyCode.E) && bOnMove && bOnCollsion)
        {
          DropObject();
        }

       MovingObject();
    }
    void MovingObject()
    {  
      if( bOnMove && _objectMove)
      {
         _objectMove.GetComponent<Rigidbody>().Sleep();
         _objectMove.transform.position = _arms.transform.position;
         _objectMove.transform.SetParent(_arms);
      }
    }
    void DropObject()
    {
        _objectMove.GetComponent<Rigidbody>().WakeUp();
        _objectMove.transform.SetParent(null);
        _objectMove = null;
        bOnMove = false;
        bOnCollsion = false;
    }
}
