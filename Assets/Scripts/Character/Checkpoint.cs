using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        PlayerMaster player = other.GetComponent<PlayerMaster>();
        if (player)
        {
            player.checkpointLocation = other.transform.position;
            player.Save();
        }
    }
}
