using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class IntroScene : MonoBehaviour
{
    public string sceneName;
    private bool hasPressed;

    public void Update()
    {
        if(!hasPressed)
        {
            AnyButtonPressed();
        }
    }

    public void AnyButtonPressed()
    {
        if(Input.anyKeyDown)
        {
            hasPressed = true;
            LoadScene();
        }
    }

    public void LoadScene()
    {
        SceneManager.LoadScene(sceneName);
    }
}
