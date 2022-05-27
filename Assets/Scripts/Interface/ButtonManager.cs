using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using TMPro;

public class ButtonManager : MonoBehaviour, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
{
    public TextMeshProUGUI text;
    public Color onSelected;
    public Color onDeselected;
    public Color onHover;
    public Color onClick;

    private void Start()
    {
        if(text)
        {
            onDeselected = text.color;
        }
    }


    public void OnPointerClick(PointerEventData eventData)
    {        
        SetTextColor(onClick);
    }

    public void OnPointerDown(PointerEventData eventData)
    {        

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        SetTextColor(onHover);
    }

    public void OnPointerExit(PointerEventData eventData)
    {        
        SetTextColor(onDeselected);
    }

    public void OnPointerUp(PointerEventData eventData)
    {        
        SetTextColor(onDeselected);
    }

    private void SetTextColor(Color _color)
    {
        if(text)
        {
            text.color = _color;
        }
    }
}
