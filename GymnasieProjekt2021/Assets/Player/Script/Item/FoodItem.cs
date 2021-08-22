using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FoodItem", menuName = "ScriptableObjects/items/new FoodItem")]
public class FoodItem : Item
{
    private void OnEnable() {
        itemType = ItemType.Food;
    }
}
