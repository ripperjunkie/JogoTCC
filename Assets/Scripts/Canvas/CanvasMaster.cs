using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasMaster : MonoBehaviour
{
    [Header("Panel")]
    public GameObject pausePanel;
    public GameObject endGamePanel;
    [Space]
    [Header("Reference")]
    public PauseMenu pauseMenu;

    private void Start()
    {
        if(pausePanel)
        {
            pausePanel.SetActive(false);
        }

        if(endGamePanel)
        {
            endGamePanel.SetActive(false);
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }


    public void OpenLevel(string _levelName)
    {
        SceneManager.LoadScene(_levelName);
    }

    public void Unpause()
    {
        if(pauseMenu)
        {
            pauseMenu.Unpause();
        }
    }
}
