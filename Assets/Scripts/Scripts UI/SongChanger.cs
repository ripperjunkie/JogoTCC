using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SongChanger : MonoBehaviour
{
    //public bool teste = false;
    //private GameObject[] valores;
    public AudioClip newSong;
    
    private void Awake()
    {
      //  valores = GameObject.FindGameObjectsWithTag("SoundBox");
        //Debug.Log(valores);
    }

    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") )
        {
            SongManager.instance.ChangeSong(newSong);

        }
        if (gameObject.CompareTag("Player"))

        {/*
            for (int i = 0; i <= valores.Length; i++)
            {
                valores[i].SetActive(true);
                if(i> valores.Length)
                {
                    gameObject.SetActive(false);
                }
            }
           */ 
            
        }
        
    }
    

    
   
    
}
