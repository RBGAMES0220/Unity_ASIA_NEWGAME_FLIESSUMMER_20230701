using GLORY;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemOnDrag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Transform originaParent;

    public Inventory maBag;
    private int currentItemID;

    public void OnBeginDrag(PointerEventData eventData)
    {
        originaParent = transform.parent;
        currentItemID = originaParent.GetComponent<Slot>().slotID;
        transform.SetParent(transform.parent. parent);
        transform.position = eventData.position;
        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
        //Debug.Log(eventData.pointerCurrentRaycast.gameObject.name);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (eventData.pointerCurrentRaycast.gameObject != null)
        {

            if (eventData.pointerCurrentRaycast.gameObject.name == "item Image")
            {
                transform.SetParent(eventData.pointerCurrentRaycast.gameObject.transform.parent.parent);
                transform.position = eventData.pointerCurrentRaycast.gameObject.transform.parent.parent.position;

                var temp = maBag.itemList[currentItemID];
                maBag.itemList[currentItemID] = maBag.itemList[eventData.pointerCurrentRaycast.gameObject.GetComponentInParent<Slot>().slotID];
                maBag.itemList[eventData.pointerCurrentRaycast.gameObject.GetComponentInParent<Slot>().slotID] = temp;




                eventData.pointerCurrentRaycast.gameObject.transform.parent.position = originaParent.position;
                eventData.pointerCurrentRaycast.gameObject.transform.parent.SetParent(originaParent);
                GetComponent<CanvasGroup>().blocksRaycasts = true;
                return;
            }

            if (eventData.pointerCurrentRaycast.gameObject.name == "slot(Clone)")
            {

                transform.SetParent(eventData.pointerCurrentRaycast.gameObject.transform);
                transform.position = eventData.pointerCurrentRaycast.gameObject.transform.position;

                maBag.itemList[eventData.pointerCurrentRaycast.gameObject.GetComponentInParent<Slot>().slotID] = maBag.itemList[currentItemID];
                if (eventData.pointerCurrentRaycast.gameObject.GetComponent<Slot>().slotID != currentItemID)
                    maBag.itemList[currentItemID] = null;


                GetComponent<CanvasGroup>().blocksRaycasts = true;
                return;
            }
        }

        transform.SetParent(originaParent);
        transform.position = originaParent.position;
        GetComponent<CanvasGroup>().blocksRaycasts = true;

    }
  
}


