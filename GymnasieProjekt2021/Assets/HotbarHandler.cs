using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotbarHandler : MonoBehaviour
{
    Build buildScript;
    InventoryManager inventoryManager;
    private void Start() {
        buildScript = GetComponent<Build>();
        inventoryManager = GetComponent<InventoryManager>();
    }
    public void OnHotbarDelta(){
        buildScript.isEnabled = false;
        StructurePreview.UpdatePreview(false);

        try{       
            switch(inventoryManager.Slots[inventoryManager.hotBarIndex].item.itemType){
                case ItemType.Structure:
                buildScript.isEnabled = true;
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
        }catch{
            Debug.Log("Caught");
        }
    }
}
