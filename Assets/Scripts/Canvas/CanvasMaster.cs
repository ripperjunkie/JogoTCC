using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasMaster : MonoBehaviour
{
    public GameObject pausePanel;
    public PauseMenu pauseMenu;

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
