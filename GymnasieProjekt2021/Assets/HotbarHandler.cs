using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotbarHandler : MonoBehaviour
{
    Build buildScript;
    InventoryManager inventoryManager;
    private void Start() {
        buildScript = GetComponent<Build>(); // FIXA SYSTEMET LATMASK
        inventoryManager = GetComponent<InventoryManager>();
    }
    public void OnHotbarDelta(){
        if(!inventoryManager.Slots[inventoryManager.hotBarIndex].isTaken) return;

        switch(inventoryManager.Slots[inventoryManager.hotBarIndex].item.itemType){
            case ItemType.Structure:
            Debug.Log("BYGG");
            break;
            case ItemType.Food:
            Debug.Log("Nom Nom");
            break;
            case ItemType.Material:
            Debug.Log("Material");
            break;
            default:
            Debug.Log("TOMT");
            break;
        }
    }
}
