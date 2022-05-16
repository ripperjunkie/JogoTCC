using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teste : MonoBehaviour
{
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
