using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConceptArtManager : MonoBehaviour
{
    public GameObject artPanel;

    public Dictionary<string, bool> unlockedConcepts = new Dictionary<string, bool>();
    public bool debug;
    public bool loadSave;


    private ConceptArtContainer _conceptArtContainer;
    private GameProgress _gameProgress;
    private GameObject conceptArtToHide;

    private void Awake()
    {
        _conceptArtContainer = FindObjectOfType<ConceptArtContainer>();
    }

    private void Start()
    {
        _gameProgress = GetComponent<GameProgress>();
        if(loadSave)
        {
            _gameProgress.saveSystem.LoadConceptArt();
            if(_gameProgress.saveSystem.LoadConceptArt() != null) 
            {
                ConceptArtData _conceptArtData = _gameProgress.saveSystem.LoadConceptArt();
                foreach (var item in _conceptArtData.unlockedConcepts)
                {
                   // print(item);
                    unlockedConcepts.Add(item.Key, item.Value);
                }
            }

        }
        if (debug)
        {
            foreach(var item in unlockedConcepts)
            {
              //  print(item);
            }
        }
       
    }


    public void LoadImage(string _imageName)
    {        
        bool hasValue = unlockedConcepts.TryGetValue(_imageName, out hasValue);
        if(hasValue)
        {
            artPanel.SetActive(true);
            // print("found value");
            ShowImage(_imageName);
            return;
        }
        print("value not found");

    }

    private void ShowImage(string _imageName)
    {
        if (!_conceptArtContainer) return;

        foreach (var item in _conceptArtContainer.artGOs)
        {
            print(item.name);
            if(_imageName == item.name)
            {
                item.SetActive(true);
                conceptArtToHide = item;
                break;
            }
        }

    }

    public void HideImage()
    {
        if(conceptArtToHide)
        {
            conceptArtToHide.SetActive(false);
        }
    }


}
