using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasFadeAnimation : MonoBehaviour
{
    public Animator fadeAnimatorController;

    private void Start()
    {
        PlayFadeIn();
    }

    public void PlayFadeIn()
    {
        fadeAnimatorController.SetTrigger("FadeIn");
    }
    public void PlayFadeOut()
    {
        fadeAnimatorController.SetTrigger("FadeOut");
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
