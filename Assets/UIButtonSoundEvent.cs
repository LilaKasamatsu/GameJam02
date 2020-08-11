using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIButtonSoundEvent : MonoBehaviour, IPointerEnterHandler, IPointerDownHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        AudioManager.instance.PlaySound("Button Hover");

    }

    public void OnPointerDown(PointerEventData eventData)
    {
        AudioManager.instance.PlaySound("Button Press");
    }

}
