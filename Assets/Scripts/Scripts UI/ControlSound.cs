using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlSound : MonoBehaviour
{
    //public bool teste;
    public GameObject[] soundBoxes;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            soundBoxes[0].SetActive(true);
            soundBoxes[1].SetActive(false);
        }
    }
}
