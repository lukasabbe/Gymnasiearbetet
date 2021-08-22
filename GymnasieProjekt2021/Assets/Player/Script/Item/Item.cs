using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Item : ScriptableObject
{
    public int id;
    public string ItemName;
    public string description;
    public Sprite Sprite;

    public ItemType itemType;
}

[System.Serializable]
public enum ItemType {
    Food,
    Structure,
    Material
}
