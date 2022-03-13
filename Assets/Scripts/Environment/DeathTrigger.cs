using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Collider))]
public class DeathTrigger : MonoBehaviour
{
    private Canvas canvas;
    private void Start()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            GameObject.FindObjectOfType<Canvas>().GetComponent<CanvasMaster>().PlayFadeAnimation(true);
        }
    }
}
