using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegionChangeSound : MonoBehaviour
{
    public AudioSource Song1;
    public AudioSource Song2;

    bool noInfinite;
    bool noInfinite2;
    // Start is called before the first frame update
    void Start()
    {
        noInfinite = false;
        noInfinite2 = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.CompareTag("SoundRange1"))
        {
            noInfinite2 = true;
            if (noInfinite)
            {
                Song1.enabled = true;
                Song2.enabled = false;
            }
            if (noInfinite)
            {
                noInfinite = !noInfinite;
            }
        }
        
        if (other.gameObject.CompareTag("SoundRange2"))
        {
            noInfinite = true;
            if (noInfinite2)
            {
                Song1.enabled = false;
                Song2.enabled = true;
                Song2.Play();
            }
            if (noInfinite2)
            {
                noInfinite2 = !noInfinite2;
            }
        }
    }


}
