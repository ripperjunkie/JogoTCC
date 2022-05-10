using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ManagerScene : MonoBehaviour
{
    public string sceneSelection;
    public AudioSource sfx;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SfxClick()
    {
        sfx.PlayOneShot(sfx.clip);
    }
    public void ChangeScene()
    {
        SceneManager.LoadScene(sceneSelection);
    }
}
