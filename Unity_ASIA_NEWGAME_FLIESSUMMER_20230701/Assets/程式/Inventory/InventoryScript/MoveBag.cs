using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MoveBag : MonoBehaviour,IDragHandler
{
    public Canvas canvas;
    RectTransform currenRect;

    public void OnDrag(PointerEventData eventData)
    {
        currenRect.anchoredPosition += eventData.delta;
    }

    void Awake()
    {
        currenRect = GetComponent<RectTransform>();
    }
}
