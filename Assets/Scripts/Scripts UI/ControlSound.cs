using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlSound : MonoBehaviour
{
    //public bool teste;
    public GameObject[] soundBoxes;
    public GameObject theSoundOfBox;
    private bool stopper;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!stopper)
            {
                for (int i = 0; i < soundBoxes.Length; i++)
                {
                    soundBoxes[i].SetActive(true);
                }
                stopper = !stopper;
                theSoundOfBox.SetActive(false);
            }

        }
    }
    private void OnDisable()
    {
        stopper = false;
    }
}
