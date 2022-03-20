using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObjects : MonoBehaviour
{
  public bool bOnMove = false;
  public  bool bOnCollsion = false;
  public GameObject objectMove = null;
  public GameObject player = null;
  
  void OnTriggerEnter(Collider col)
  {
      if(col.gameObject.CompareTag("Box"))
      {
        bOnCollsion = true;
        objectMove = col.gameObject;
      }   
  }
  void OnTriggerExit(Collider col)
  {
    if(col.gameObject.CompareTag("Box"))
    {
        bOnCollsion = false;
        objectMove = null;
    }

  }

    void Update()
    {
        if(Input.GetKey(KeyCode.LeftShift) && !bOnMove && bOnCollsion)
        {        
          bOnMove = true;
        }
        else
        {
          bOnMove = false;
        }

        MovingObject();
    }

    void MovingObject()
    { 
      if(bOnMove)
      {
       objectMove.transform.position = Vector3.MoveTowards(player.transform.position, player.transform.position, 2);
      }

    }
}
