using UnityEngine;

[RequireComponent(typeof(Collider))]
public class DeathTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        PlayerMaster player = other.GetComponent<PlayerMaster>();
        if(player)
        {
            player.Die();
        }
    }
}
