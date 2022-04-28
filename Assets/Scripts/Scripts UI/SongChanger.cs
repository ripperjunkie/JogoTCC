using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SongChanger : MonoBehaviour
{
    public AudioClip newSong;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SongManager.instance.ChangeSong(newSong);
        }
    }
}
