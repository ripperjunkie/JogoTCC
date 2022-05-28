using UnityEngine;

[RequireComponent(typeof(Collider))]
public class UnlockConceptArtTrigger : MonoBehaviour
{
    public string conceptArtName;

    private void OnTriggerEnter(Collider other)
    {
        PlayerMaster playerMaster = other.GetComponent<PlayerMaster>();
        if(playerMaster)
        {
            playerMaster.gameProgress.conceptArtManager.unlockedConcepts.Add(conceptArtName, true);
            playerMaster.saveSystem.SaveConceptArt();
        }
    }
}
