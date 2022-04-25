using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public enum ECheckMethod
{
    DISTANCE,
    TRIGGER
}
public class LevelStreaming : MonoBehaviour
{
    public Transform player;
    public ECheckMethod checkMethod;
    public float loadRange;

    [SerializeField] private bool isLoaded;
    [SerializeField] private bool shouldLoad;


    private void Start()
    {
        player = FindObjectOfType<PlayerMaster>().gameObject.transform;

        if(SceneManager.sceneCount > 0)
        {
            for (int i = 0; i < SceneManager.sceneCount; i++)
            {
                Scene scene = SceneManager.GetSceneAt(i);
                if(scene.name == gameObject.name)
                {
                    isLoaded = true;
                }
            }
        }
    }

    private void Update()
    {
        switch(checkMethod)
        {
            case ECheckMethod.DISTANCE:
                DistanceCheck();
            break;

            case ECheckMethod.TRIGGER:
                TriggerCheck();
            break;
        }
        print("Distance: " + Vector3.Distance(player.position, transform.position));

    }

    private void DistanceCheck()
    {
        if (!player) return;

        if(Vector3.Distance(player.position, transform.position) < loadRange)
        {
            LoadScene();
        }
        else
        {
            UnloadScene();
        }
    }

    private void TriggerCheck()
    {
        if (shouldLoad)
        {
            LoadScene();
        }
        else
        {
            UnloadScene();
        }
    }

    private void LoadScene()
    {
        if(!isLoaded)
        {
            SceneManager.LoadSceneAsync(gameObject.name, LoadSceneMode.Additive);
            isLoaded = true;
        }
    }

    private void UnloadScene()
    {
        if (isLoaded)
        {
            SceneManager.UnloadSceneAsync(gameObject.name);
            isLoaded = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            shouldLoad = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            shouldLoad = false;
        }

    }

}
