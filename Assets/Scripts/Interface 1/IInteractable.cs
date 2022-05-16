using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable<T>
{
    void OnInteract(T _objectSender);
    void OnInteract(PlayerMaster playerMaster);
}
