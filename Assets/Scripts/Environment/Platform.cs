using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    private GameObject player = null;
    private bool active = false;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void OnTriggerStay(Collider col)
    {
        if (col.gameObject == player)
        {
            active = true;
            player.transform.parent = transform;
        }
    }
    void OnTriggerExit(Collider col)
    {
        if (col.gameObject == player)
        {
            active = false;
            player.transform.parent = null;
        }

    }
}
