using UnityEngine;
using UnityEngine.SceneManagement;


public class MenuManager : MonoBehaviour
{
    public AudioSource sfx;

    private void Start()
    {
        
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
