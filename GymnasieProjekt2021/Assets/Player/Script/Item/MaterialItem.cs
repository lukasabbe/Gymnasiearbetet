using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MaterialItem", menuName = "ScriptableObjects/items/new MaterialItem")]
public class MaterialItem : Item
{
    private void OnEnable()
    {
        itemType = ItemType.Material;
    }
}
