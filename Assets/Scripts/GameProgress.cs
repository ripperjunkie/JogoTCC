using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameProgress : MonoBehaviour
{
    public SaveSystem saveSystem = new SaveSystem();
    public ConceptArtManager conceptArtManager;

    public void Awake()
    {
        conceptArtManager = GetComponent<ConceptArtManager>();
    }

}
