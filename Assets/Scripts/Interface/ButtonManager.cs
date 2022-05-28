using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
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


    private void Awake()
    {
        if(text)
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
        if(text)
        {
            text.color = _color;
        }
    }

    private void SetTextSize(float _size)
    {
        if (!shrinkEffect) return;
        if(text)
        {
            text.fontSize = _size;
        }
    }
}
