using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FoodItem", menuName = "ScriptableObjects/items/new FoodItem")]
public class FoodItem : Item
{
    public ItemType type = ItemType.Food;
}
