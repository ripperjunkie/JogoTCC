using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;


public class ButtonManager : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
{
    public TextMeshProUGUI text;
    [Header("Color")]
    public Color onSelected;
    public Color onDeselected;
    public Color onHover;
    public Color onClick;

    [Header("Shrink Effect")]
    public bool shrinkEffect;
    private float defaultTxtSize;
    public float onHoverSizeTxt;
    public float onClickSizeTxt;

    public bool shouldDeactivate;

    private Button _button;
    public float alpha;
    private void Awake()
    {
        _button = GetComponent<Button>();
        GameProgress gameProgress = FindObjectOfType<GameProgress>();
        if(gameProgress && shouldDeactivate)
        {
            _button.interactable = gameProgress.saveSystem.CheckSaveExist();
            if(!gameProgress.saveSystem.CheckSaveExist())
            {
                text.alpha = alpha;
                return;
            }
        }



        text = GetComponentInChildren<TextMeshProUGUI>();
        if (text)
        {
            onDeselected = text.color;
            defaultTxtSize = text.fontSize;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        SetTextColor(onClick);
        SetTextSize(onClickSizeTxt);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        SetTextColor(onHover);
        SetTextSize(onHoverSizeTxt);
    }

    public void OnPointerExit(PointerEventData eventData)
    {        
        SetTextColor(onDeselected);
        SetTextSize(defaultTxtSize);
    }

    public void OnPointerUp(PointerEventData eventData)
    {        
        SetTextColor(onDeselected);
        SetTextSize(defaultTxtSize);
    }

    private void SetTextColor(Color _color)
    {
        if (!_button.IsInteractable())
            return;

        if(text)
        {
            text.color = _color;
        }
    }

    private void SetTextSize(float _size)
    {
        if (!shrinkEffect || !_button.IsInteractable()) return;
        if(text)
        {
            text.fontSize = _size;
        }
    }
}
