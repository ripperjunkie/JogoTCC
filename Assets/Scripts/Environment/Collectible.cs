using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Collectible : MonoBehaviour, IInteractable<PlayerMaster>
{
    public ECollectibe collectible;

    public void OnInteract(PlayerMaster _objectSender)
    {
        if(_objectSender)
        {
            _objectSender.collectiblesFound.Add(collectible, true);
            Destroy(gameObject);
        }
    }
}
