using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushLever : MonoBehaviour
{
  public GameObject plataform = null;
  public GameObject player = null;
  public  Transform EndPoint;
  private Vector3 initialPoint;
  public float speedPlataform = 0;
  public bool active = false;
  public bool begin = false;

      void Start()
    {
        initialPoint= plataform.transform.position;
    }
   void OnTriggerEnter(Collider col)
  {
      if(col.gameObject == player)
      {
         active= true;
         player.transform.parent = transform;        
      }   
  }
  void OnTriggerExit(Collider col)
  {
    if(col.gameObject == player)
    {
       active= false;  
        player.transform.parent = null;   
    }

  }
  void Update()
  {
    if(Input.GetKeyDown(KeyCode.E) && active)
      {
         ChangeRoute();
         StartCoroutine(ActiveLever());
      }
  }

    IEnumerator ActiveLever()
  {
      if(begin)
      {
        while(Vector3.Distance(plataform.transform.position, EndPoint.transform.position) > 0)
          {
            plataform.transform.position =Vector3.Lerp(plataform.transform.position,EndPoint.transform.position, speedPlataform* Time.deltaTime);         
              yield return new WaitForSeconds(0.02f);
          }
      }
   
      else 
      {
          while(Vector3.Distance(plataform.transform.position, initialPoint) > 0)
        {
          plataform.transform.position = Vector3.Lerp(plataform.transform.position, initialPoint, speedPlataform * Time.deltaTime);       
           yield return new WaitForSeconds(0.02f);
        }
      }
  }
  void ChangeRoute()
  {
    if(begin)
    {
      begin = false;
    }
    else
    {
      begin = true;
    }
  }
}
