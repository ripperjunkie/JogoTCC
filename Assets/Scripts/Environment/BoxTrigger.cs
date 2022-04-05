using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxTrigger : MonoBehaviour
{
    public TriggerEventFase triggerEvent;
    [SerializeField] private string tag;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag(tag))
        {
            print("hello");
            if(triggerEvent != null)
            {
                triggerEvent.Invoke();
                print("hello2");
            }
        }
    }
}
