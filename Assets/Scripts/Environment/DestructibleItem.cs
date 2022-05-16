using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleItem : MonoBehaviour, IInteractable<PlayerMaster>
{
    public void OnInteract(PlayerMaster _objectSender)
    {
        Destroy(gameObject); 
    }
}
