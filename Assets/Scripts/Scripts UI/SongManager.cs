using System.Collections;
using UnityEngine;

public class SongManager : MonoBehaviour
{
    public AudioClip firstSong;
    private AudioSource audioSource;
    private bool isPlayingSong01;

    public static SongManager instance;

    private void Awake()
    {
        if (instance == null) instance = this;
        audioSource = GetComponent<AudioSource>();
    }
    private void Start()
    {
        isPlayingSong01 = true;
        StartCoroutine(EFirstSong());
    }
    public void ChangeSong(AudioClip _newClip1)
    {
        StopAllCoroutines();
        StartCoroutine(FadeEffect(_newClip1));
        isPlayingSong01 = !isPlayingSong01;
    }
    private IEnumerator FadeEffect(AudioClip _newClip)
    {
        float fadeOutTimer = 2.25f;
        float currentTimer = 0;

        if (isPlayingSong01)
        {
            audioSource.clip = _newClip;
            audioSource.Play();

            while(currentTimer < fadeOutTimer)
            {
                audioSource.volume = Mathf.Lerp(1f, 0, currentTimer / fadeOutTimer);
                audioSource.volume = Mathf.Lerp(0, 1f, currentTimer / fadeOutTimer);
               // Debug.Log(currentTimer);
                currentTimer += Time.deltaTime;
                yield return null;
            }
            
            //audioSource.Stop();
        }
        else
        {
            audioSource.clip = _newClip;
            audioSource.Play();
            while (currentTimer < fadeOutTimer)
            {
                audioSource.volume = Mathf.Lerp(1f, 0, currentTimer / fadeOutTimer);
                audioSource.volume = Mathf.Lerp(0, 1f, currentTimer / fadeOutTimer);
                currentTimer += Time.deltaTime;
                yield return null;
            }
            //audioSource.Stop();
        }
    }
    private IEnumerator EFirstSong()
    {
        yield return new WaitForSeconds(2.2f);
        ChangeSong(firstSong);
    }
}
