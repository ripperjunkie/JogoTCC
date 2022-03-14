using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inicial : MonoBehaviour
{
    //anexar ao Audio Source
    [SerializeField]AudioSource m_AudioSource;
    float valueSlider;
    bool canIncrease;


    void Start()
    {
        //para poder começar a aumentar a musica
        canIncrease = true;
        valueSlider = 0f;    
    }

    // Update is called once per frame
    void Update()
    {
        //mudar o valor e periodicamente atualiza-lo
        m_AudioSource.volume = valueSlider;
        //condição para aumentar o volume
        if (valueSlider < 0.25f && canIncrease)
        {
            StartCoroutine(AumentandoVolume());
        }
        
    }
    //coroutine para aumentar o volume suavemente
    IEnumerator AumentandoVolume()
    {
        canIncrease = false;
        yield return new WaitForSeconds(0.3f); 
        valueSlider += 0.02f;
        yield return new WaitForSeconds(0.3f);
        canIncrease = true;
    }
}
