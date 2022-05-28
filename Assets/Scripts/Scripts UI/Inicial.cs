using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inicial : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject primaryMenu;
    [Range(0,0.34f)]public float limitMusicIncrease;
    //anexar ao Audio Source
    [SerializeField] AudioSource _AudioSource1;
    [SerializeField] AudioSource _AudioSource2;
    float _valueSlider;
    bool _canIncrease;

    bool _MenuScreen,_PrimaryScreen;

    void Start()
    {
       
        _MenuScreen = true;
        _PrimaryScreen = false;  
        //para poder começar a aumentar a musica
        _canIncrease = true;
        _valueSlider = 0f;    
    }

    // Update is called once per frame
    void Update()
    {
        //mudar o valor e periodicamente atualiza-lo
        if(mainMenu)
        {
            if (mainMenu.activeSelf)
                _AudioSource1.volume = _valueSlider;
        }

        if(primaryMenu)
        {
            if (primaryMenu.activeSelf)
            {
                _AudioSource2.volume = _valueSlider;
            }
        }

        //condição para aumentar o volume
        if (_valueSlider < limitMusicIncrease && _canIncrease)
        {
            StartCoroutine(EVolumeIncrease());
        }
        //chamando função para trocar a tela do menu
        //AnyButtonPressed();
        
    }

    public void AnyButtonPressed()
    {
        if(Input.anyKeyDown && _MenuScreen)
        {
            //Debug.Log("uma vez");
            //ativa o Menu principal      precisa por efeito sonoro de feedback que ele apertou algum botão
            mainMenu.SetActive(false);
            primaryMenu.SetActive(true);
            _PrimaryScreen = true;
            _valueSlider = 0f;
            limitMusicIncrease = 0.3f;
            //aqui desativa a musica de abertura preciso por um timer de fade e acrescentando o efeito sonoro pulo para próxima música
            _AudioSource1.enabled = false;
            _AudioSource2.enabled = true;
            _MenuScreen = false; 
        }
    }

    //coroutine para aumentar o volume suavemente
    IEnumerator EVolumeIncrease()
    {
        _canIncrease = false;
        yield return new WaitForSeconds(0.3f); 
        _valueSlider += 0.02f;
        yield return new WaitForSeconds(0.3f);
        _canIncrease = true;
    }
}
