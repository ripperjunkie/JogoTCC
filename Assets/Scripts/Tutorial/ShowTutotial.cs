using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ShowTutotial : MonoBehaviour
{
    public float timeToShow;
    private float timeLast;
    public float timeAnim;
    //public Sprite spriteToShow;

    private bool inside;

    public GameObject canvas;

    //public Image image;


    void Awake()
    {
        //image.sprite = spriteToShow;

        timeLast = timeToShow;
    }

    private void Update()
    {
        if (inside)
        {
            if(timeLast <= 0)
            {
                ShowTutorial();
                return;
            }
            timeLast -= Time.deltaTime;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inside = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inside = false;
            CloseTutorial();
        }
    }

    private void ShowTutorial()
    {
        inside = false;
        canvas.SetActive(true);
        canvas.transform.localScale = Vector3.zero;
        canvas.transform.LeanScale(Vector3.one, timeAnim).setEaseInOutQuart();
    }
    private void CloseTutorial()
    {
        canvas.transform.localScale = Vector3.one;
        canvas.transform.LeanScale(Vector3.zero, timeAnim).setEaseInOutQuart().setOnComplete(CanvasOff);
    }

    private void CanvasOff()
    {
        canvas.SetActive(false);
    }
}
