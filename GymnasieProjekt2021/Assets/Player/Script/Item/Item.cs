using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Item : ScriptableObject
{
    public string id;
    public string ItemName;
    public string description;
    public Sprite Sprite;
    public GameObject gameobj;
    public ItemType itemType;
}

[System.Serializable]
public enum ItemType {
    Food,
    Structure,
    Material,
    Tool
}
