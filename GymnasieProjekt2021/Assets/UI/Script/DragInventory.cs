using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragInventory : MonoBehaviour , IPointerDownHandler , IBeginDragHandler , IEndDragHandler , IDragHandler
{
    public Canvas canvas;
    private RectTransform rectTransform;
    public InventoryManger invtory;
    private int startPos = 0;
    private int endPos;
    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
    
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        endPos = invtory.findNerestSlot(eventData.position);
        invtory.changeSlot(startPos, endPos);
        startPos = endPos;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
    }

}
