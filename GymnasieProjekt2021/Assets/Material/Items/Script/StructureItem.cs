using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="StructereItem", menuName = "ScriptableObjects/items/new StructereItem")]
public class StructureItem : Item
{
    public StructureObject structure;

    private void OnEnable() {
        itemType = ItemType.Structure;
    }
}
