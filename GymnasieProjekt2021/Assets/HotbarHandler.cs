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
        if (!inventoryManager.Slots[inventoryManager.hotBarIndex].isTaken) {StructurePreview.RemovePreivew(); buildScript.enabled = false; return; }

        switch(inventoryManager.Slots[inventoryManager.hotBarIndex].item.itemType){
            case ItemType.Structure:
                buildScript.enabled = true;
                StructureItem st = (StructureItem)inventoryManager.Slots[inventoryManager.hotBarIndex].item;
                buildScript.structure = st.structure;
                Debug.Log("BYGG");
            break;
            case ItemType.Food:
                buildScript.enabled = false;
                Debug.Log("Nom Nom");
            break;
            case ItemType.Material:
                buildScript.enabled = false;
                Debug.Log("Material");
            break;
            default:
                Debug.Log("TOMT");
            break;
        }
    }
}
