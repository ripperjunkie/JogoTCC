using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleSeesaw : MonoBehaviour
{
     [SerializeField] private int _numberCollider;
     private bool ss = false;
    public List <GameObject> objts;
    public TriggerEventFase  triggerEvent;
    [SerializeField] private string tag;

      void OnTriggerStay(Collider  col)
      {
        if(col.CompareTag(tag))
        {
          if(! objts.Contains(col.gameObject))
          {
             objts.Add(col.gameObject);
            Move();
          }
        }
      } 
      void OnTriggerExit(Collider  col)
      {
        if(col.CompareTag(tag))
        {
          if(objts.Contains(col.gameObject))
          {
             objts.Remove(col.gameObject);
            Move();
          }
        }
      }      
    void Start()
    {
        
    }
    void Move()
    {
      if(objts.Count == _numberCollider)
      {
         if(triggerEvent != null)
          {
            triggerEvent.Invoke();
          }
      }     
    }

}
