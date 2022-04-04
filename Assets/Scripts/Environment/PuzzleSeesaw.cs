using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleSeesaw : MonoBehaviour
{
    public  GameObject [] Cargas;
    public int contCargas;
    
    void OnTriggerStay(Collider  gg)
    {
        for(int i = 0; i < 5; i++)
        {
             if(gg.gameObject.CompareTag("Box") ||  gg.gameObject.CompareTag("Player")  || gg.gameObject.CompareTag("BoxMove"))
            {       
                for(int j= 0; j < contCargas; j++)
                {
                    Cargas[j] = gg.gameObject;
                    i++;
                    j++;
                }
            }  
        }
        
    }
       void OnTriggerExit(Collider col)
    {

    }
    void Start()
    {
        
    }
     void ContSeesaw()
     {

     }


    void Update()
    {
        
    }
}
