using UnityEngine;
using UnityEngine.SceneManagement;


public class MenuManager : MonoBehaviour
{
    public AudioSource buttonPressedSound;
    private GameProgress _gameProgress;

    private void Awake()
    {
        _gameProgress = FindObjectOfType<GameProgress>();
    }

    private void Start()
    {
        Time.timeScale = 1.0f;
        buttonPressedSound = GetComponent<AudioSource>();
        print(Application.persistentDataPath);
    }

    public void PlayButtonSound()
    {
        buttonPressedSound.PlayOneShot(buttonPressedSound.clip);
    }
    public void ChangeScene(string _sceneName)
    {
        SceneManager.LoadScene(_sceneName);
    }

    public void NewGame()
    {
        GameProgress.shouldLoadSave = false;
    }

    public void LoadGame()
    {
        GameProgress.shouldLoadSave = true;
    }


}
