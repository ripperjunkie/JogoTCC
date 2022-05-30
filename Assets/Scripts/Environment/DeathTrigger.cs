using UnityEngine;

[RequireComponent(typeof(Collider))]
public class DeathTrigger : MonoBehaviour
{
    private void Start()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        PlayerMaster player = other.GetComponent<PlayerMaster>();
        if(player && !player.godMode)
        {
            FindObjectOfType<CanvasFadeAnimation>().PlayFadeOut();
        }
    }
}
