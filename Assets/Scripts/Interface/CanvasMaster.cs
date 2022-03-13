using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasMaster : MonoBehaviour
{
    public Animator fadeAnimatorController;

    public void PlayFadeAnimation(bool fadeIn)
    {
        fadeAnimatorController.speed = fadeIn ? 1f : -1f;
        fadeAnimatorController.ResetTrigger("FadeIn");
    }
}
