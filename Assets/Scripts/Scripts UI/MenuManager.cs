using UnityEngine;
using UnityEngine.SceneManagement;


public class MenuManager : MonoBehaviour
{
    public AudioSource sfx;

    private void Start()
    {
        Time.timeScale = 1.0f;
    }

    private void Update()
    {
        
    }

    public void SfxClick()
    {
        sfx.PlayOneShot(sfx.clip);
    }
    public void ChangeScene(string _sceneName)
    {
        SceneManager.LoadScene(_sceneName);
    }
}
