using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushLever : MonoBehaviour
{
 
  public float speedPlataform = 0;
  public bool active = false;
  
   public  Platform platform;

      void Start()
    {
    
    }
       void OnTriggerEnter(Collider col)
  {
      if(col.gameObject.CompareTag("Player"))
      {
         active= true;
      }   
  }
         void OnTriggerExit(Collider col)
  {
      if(col.gameObject.CompareTag("Player"))
      {
         active= false;
      }   
  }

  void Update()
  {
    if(Input.GetKeyDown(KeyCode.E) && active)
      {
        
       Move();
      }
  }
  void Move()
  {
    if(active)
    {
      platform.StartCoroutine(platform.ActiveLever());
    }
    
  }

}
