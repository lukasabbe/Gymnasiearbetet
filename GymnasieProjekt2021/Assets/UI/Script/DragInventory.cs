using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragInventory : MonoBehaviour , IPointerDownHandler , IBeginDragHandler , IEndDragHandler , IDragHandler
{
    public Canvas canvas;
    private RectTransform rectTransform;
    public InventoryManager invtory;
    private int startPos = 0;
    private int endPos;
    private bool isItemInfoOn = false;
    public bool dragOn = false;
    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        startPos = invtory.findEmptySlot() -1;
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        isItemInfoOn = true;
        dragOn = true;
        InfoPanel(eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        endPos = invtory.findNerestSlot(eventData.position);
        bool changedSlot = invtory.changeSlot(startPos, endPos);
        if(changedSlot) startPos = endPos;
        dragOn = false;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        StartCoroutine(infoPanelWait(eventData));
    }
    IEnumerator infoPanelWait(PointerEventData eventData)
    {
        yield return new WaitForSeconds(0.09f);
        if (!dragOn) InfoPanel(eventData);
    }

    public void InfoPanel(PointerEventData eventData)
    {
        if (isItemInfoOn == false)
        {
            invtory.setItemInfo(invtory.findNerestSlot(eventData.position), true);
            isItemInfoOn = true;
        }
        else if (isItemInfoOn)
        {
            invtory.setItemInfo(invtory.findNerestSlot(eventData.position), false);
            isItemInfoOn = false;
        }
    }
}
