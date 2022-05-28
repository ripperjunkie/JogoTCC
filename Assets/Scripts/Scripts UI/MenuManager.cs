using UnityEngine;
using UnityEngine.SceneManagement;


public class MenuManager : MonoBehaviour
{
    public AudioSource buttonPressedSound;

    private void Start()
    {
        Time.timeScale = 1.0f;
    }

    public void SfxClick()
    {
        buttonPressedSound.PlayOneShot(buttonPressedSound.clip);
    }
    public void ChangeScene(string _sceneName)
    {
        SceneManager.LoadScene(_sceneName);
    }
}
