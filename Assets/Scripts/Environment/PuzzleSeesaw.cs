using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleSeesaw : MonoBehaviour
{
     [SerializeField] private int _numberCollider;
    public List <GameObject> objts;
    public TriggerEventFase  triggerEvent;
    public ResetTriggerEventFase resetTriggerEvent;

    [SerializeField] private string tag;

      void OnTriggerEnter(Collider  col)
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
            Remove();
          }
        }
      }
      
  
    private void Remove()
    {    
       if(objts.Count != _numberCollider)
      {
        if (resetTriggerEvent != null)
        {
            resetTriggerEvent.Invoke();         
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
          }
      }     
    }

}
