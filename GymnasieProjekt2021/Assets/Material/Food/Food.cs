using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{   
    [HideInInspector]
    public FoodItem ft = null;

    public float timeToEat;
    public float mouseDownT;
    bool mouseDown = false;
    InventoryManager inventoryManager;
    PlayerInputEventManager input;

    private void OnEnable() {
        inventoryManager = FindObjectOfType<InventoryManager>();
        input = FindObjectOfType<PlayerInputEventManager>();

        input.leftMouseButton += OnLeftDown;
        input.leftMouseButtonUp += OnLeftUp;
    }
      private void OnDisable() {
        input = FindObjectOfType<PlayerInputEventManager>();

        input.leftMouseButton -= OnLeftDown;
        input.leftMouseButtonUp -= OnLeftUp;
    }
    
    private void Update() {
        if (mouseDown) mouseDownT += Time.deltaTime;
        else mouseDownT = 0f;

        if (mouseDownT >= timeToEat){
            inventoryManager.removeSlot(inventoryManager.hotBarIndex);
            PlayerStats.HP = Mathf.Clamp(PlayerStats.HP, 0, PlayerStats.MaxHP);
        }
    }

    void OnLeftDown(){
        mouseDown = true;
    }

    void OnLeftUp(){
        mouseDown = false;
    }
}
