using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotbarHandler : MonoBehaviour
{
    Build buildScript;
    Food foodScript;
    Tool toolScript;
    InventoryManager inventoryManager;
    private void Start() {
        buildScript = GetComponent<Build>(); // FIXA SYSTEMET LATMASK ;(
        foodScript = GetComponent<Food>();
        toolScript = GetComponent<Tool>();
        inventoryManager = GetComponent<InventoryManager>();
    }
    public void OnHotbarDelta(){
        if (!inventoryManager.Slots[inventoryManager.hotBarIndex].isTaken) {
            StructurePreview.RemovePreivew();
            buildScript.enabled = false;
            foodScript.enabled = false; 
            toolScript.enabled = false;
            return; 
        }

        switch(inventoryManager.Slots[inventoryManager.hotBarIndex].item.itemType){
            case ItemType.Structure:
                buildScript.enabled = true;
                foodScript.enabled = false;
                toolScript.enabled = false;

                StructureItem st = (StructureItem)inventoryManager.Slots[inventoryManager.hotBarIndex].item;
                buildScript.structure = st.structure;
            break;
            case ItemType.Food:
                buildScript.enabled = false;
                foodScript.enabled = true;
                toolScript.enabled = false;

                FoodItem ft = (FoodItem)inventoryManager.Slots[inventoryManager.hotBarIndex].item;
                foodScript.ft = ft;
                foodScript.timeToEat = ft.timeToEat;
            break;
            case ItemType.Material:
                buildScript.enabled = false;
                foodScript.enabled = false;  
                toolScript.enabled = false;   
                Debug.Log("Material");
            break;
            case ItemType.Tool:
                buildScript.enabled = false;
                foodScript.enabled = false;
                toolScript.enabled = true;

                ToolItem tt = (ToolItem)inventoryManager.Slots[inventoryManager.hotBarIndex].item;
                toolScript.materialType = tt.materialType;
                toolScript.teir = tt.teir;
                toolScript.damage = tt.damage;
            break;
        }
    }
}
