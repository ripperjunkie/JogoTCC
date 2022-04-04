using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpObjects : MonoBehaviour
{
 private bool bOnMove = false;
 private  bool bOnCollsion = false;
 private GameObject _arms = null;
 private GameObject _objectMove = null;

 void Start()
 {
   _arms = GameObject.Find("Arms");
 }
  
  void OnTriggerEnter(Collider col)
  {
      if(col.gameObject.CompareTag("Box"))
      {
        bOnCollsion = true;
        _objectMove = col.gameObject;
      }    
  }
    void OnTriggerExit(Collider col)
  {
      if(col.gameObject.CompareTag("Box"))
      {
        bOnCollsion = false;
      }    
  }


    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) && !bOnMove && bOnCollsion)
        {        
          bOnMove = true;
        }
        else if(Input.GetKeyDown(KeyCode.E) && bOnMove && bOnCollsion)
        {
          DropObject();
        }

        MovingObject();
    }
    void MovingObject()
    {  
      if(bOnMove && _objectMove)
      {
         _objectMove.GetComponent<Rigidbody>().useGravity = false;
         _objectMove.transform.position = Vector3.MoveTowards(_arms.transform.position, _arms.transform.position, 1);      
      }
    }
    void DropObject()
    {
        _objectMove.GetComponent<Rigidbody>().useGravity = true;
        _objectMove = null;
        bOnMove = false;
    }
}
