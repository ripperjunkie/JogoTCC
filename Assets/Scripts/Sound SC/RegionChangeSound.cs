using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegionChangeSound : MonoBehaviour
{
    public AudioSource Song1;
    public AudioSource Song2;

    private bool noInfinite;
    private bool noInfinite2;
    private bool canIncrease;
    private float sliderVol;
    private float sliderVol2;
    
    // Start is called before the first frame update
    void Start()
    {
        noInfinite = false;
        noInfinite2 = false;
        canIncrease = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.CompareTag("SoundRange1"))
        {
            SongChange(Song1, Song2, noInfinite, noInfinite2);
        }
        
        if (other.gameObject.CompareTag("SoundRange2"))
        {
            SongChange(Song2, Song1, noInfinite2, noInfinite);
        }
    }

    private void SongChange(AudioSource _audio1, AudioSource _audio2, bool _condition1, bool _condition2)
    {
       _condition2 = true;
        if (_condition1)
        {
            _audio1.enabled = true;
            _audio2.enabled = false;
            _audio2.Stop();
        }
        if (_condition1)
        {
            _condition1 = !_condition1;
        }
        if (_audio1.isPlaying == false) 
        { 
            _audio1.Play(); 
        }
        if (_audio2.isPlaying) _audio2.Stop();

    }
    private IEnumerator EVolumeUp()
    {

        canIncrease = false;
        yield return new WaitForSeconds(0.3f);
        sliderVol += 0.015f;
        yield return new WaitForSeconds(0.3f);
        canIncrease = true;
    }// ainda não terminei 

}
