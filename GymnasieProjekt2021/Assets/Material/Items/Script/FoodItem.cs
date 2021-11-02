using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FoodItem", menuName = "ScriptableObjects/items/new FoodItem")]
public class FoodItem : Item
{
    public float timeToEat = 0f;
    public int healthRecover = 0;
    public int foodRecover = 0;
    private void OnEnable() {
        itemType = ItemType.Food;
    }
}
