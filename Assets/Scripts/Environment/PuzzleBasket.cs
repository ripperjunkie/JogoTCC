using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleBasket : MonoBehaviour
{
 [SerializeField] private int _numberCollider;
 public List <GameObject> objts;
 public TriggerEventFase   triggerEvent;
    [SerializeField] private string tag;

      void OnTriggerStay(Collider  col)
      {
        if(col.CompareTag(tag))
        {
          if(! objts.Contains(col.gameObject))
          {
             objts.Add(col.gameObject);
             col.gameObject.SetActive(false);
             Move();
          }
        }
      } 
    void Move()
    {
      if(objts.Count == _numberCollider)
      {
         if(triggerEvent != null)
          {
            triggerEvent.Invoke();
            print("GG");
          }
      }       
    }
}
