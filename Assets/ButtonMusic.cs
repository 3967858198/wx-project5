using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonMusic : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        MusicManager.Instance.CreateMusic("buttonHover");
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        MusicManager.Instance.CreateMusic("buttonClick");
    }
}
