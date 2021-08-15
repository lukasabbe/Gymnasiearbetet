using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragInventory : MonoBehaviour , IPointerDownHandler 
{
    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("test");
    }

}
