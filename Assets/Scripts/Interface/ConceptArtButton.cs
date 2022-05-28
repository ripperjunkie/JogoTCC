using UnityEngine;
using UnityEngine.UI;

public class ConceptArtButton : MonoBehaviour
{
    public string conceptName;
    private Button _button;

    private ConceptArtManager _conceptArtManager;
    public Sprite conceptArtImage;

    public void Start()
    {
        _conceptArtManager = FindObjectOfType<ConceptArtManager>();
        _button = GetComponent<Button>();
        if(_button && GetConceptImage())
        {
            _button.image.sprite = conceptArtImage;
        }
    }

    public bool GetConceptImage()
    {
        if(!_conceptArtManager)
        {
            return false;
        }
        bool hasValue = _conceptArtManager.unlockedConcepts.TryGetValue(conceptName, out hasValue);

        return hasValue;
    }
}
