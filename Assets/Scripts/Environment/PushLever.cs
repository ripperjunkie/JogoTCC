using UnityEngine;



public class PushLever : MonoBehaviour
{

    public float speedPlataform = 0;
    private bool bOncolission = false;
    public bool active = false;

    
    public TriggerEventFase triggerEvent;
    public ResetTriggerEventFase reset;

    void Start()
    {

    }
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            bOncolission = true;
        }
    }
    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            bOncolission = false;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && bOncolission && !active)
        {
             active = true;
            Move();
        }
         else if (Input.GetKeyDown(KeyCode.E) && bOncolission && active)
        {
                active = false;
            Remove();
        }
    }
    void Move()
    {
      
        triggerEvent.Invoke();
        print(active);
    }
    void Remove()
    {
    
        reset.Invoke();
        print(active);
    }

}
