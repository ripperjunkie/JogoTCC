using UnityEngine;

public class TeleportTrigger : MonoBehaviour
{
    public Transform teleportTarget;

    private void OnTriggerEnter(Collider other)
    {
        PlayerMaster playerMaster = other.GetComponent<PlayerMaster>();
        if(playerMaster)
        {
            playerMaster.transform.SetPositionAndRotation(teleportTarget.position, playerMaster.transform.rotation);
        }
    }
}
