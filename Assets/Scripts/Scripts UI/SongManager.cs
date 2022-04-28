using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SongManager : MonoBehaviour
{
    public AudioClip firstSong;

    private AudioSource song01, song02;
    private bool isPlayingSong01;

    public static SongManager instance;

    private void Awake()
    {
        if (instance == null) instance = this;
    }
    private void Start()
    {
        song01 = gameObject.AddComponent<AudioSource>();
        song02 = gameObject.AddComponent<AudioSource>();
        isPlayingSong01 = true;

        ChangeSong(firstSong);
    }
    public void ChangeSong(AudioClip _newClip1)
    {
        StopAllCoroutines();
        StartCoroutine(FadeEffect(_newClip1));
        isPlayingSong01 = !isPlayingSong01;
    }
    private IEnumerator FadeEffect(AudioClip _newClip)
    {
        float fadeOutTimer = 1.75f;
        float currentTimer = 0;

        if (isPlayingSong01)
        {
            song02.clip = _newClip;
            song02.Play();

            while(currentTimer < fadeOutTimer)
            {
                song02.volume = Mathf.Lerp(0, 1, currentTimer / fadeOutTimer);
                song01.volume = Mathf.Lerp(1, 0, currentTimer / fadeOutTimer);
                Debug.Log(currentTimer);
                currentTimer += Time.deltaTime;
                yield return null;
            }

            song01.Stop();
        }
        else
        {
            song01.clip = _newClip;
            song01.Play();
            while (currentTimer < fadeOutTimer)
            {
                song01.volume = Mathf.Lerp(0, 1, currentTimer / fadeOutTimer);
                song02.volume = Mathf.Lerp(1, 0, currentTimer / fadeOutTimer);
                currentTimer += Time.deltaTime;
                yield return null;
            }
            song02.Stop();
        }
    }
}
