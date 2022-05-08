using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasMaster : MonoBehaviour
{
    public GameObject pausePanel;

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().ToString());
    }


    public void OpenLevel(string _levelName)
    {
        SceneManager.LoadScene(_levelName);
    }




}
