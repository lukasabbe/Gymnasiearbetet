using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="ToolItem", menuName = "ScriptableObjects/items/new ToolItem")]
public class ToolItem : Item
{
    public int damage;
    public int teir;
    public MaterialManager.MaterialType materialType;

    private void OnEnable() {
        itemType = ItemType.Tool;
    }
}
