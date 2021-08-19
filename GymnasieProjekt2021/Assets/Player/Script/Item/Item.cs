using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Item : ScriptableObject
{
    public int id;
    public string ItemName;
    public string description;
    public Sprite Sprite;
}

public enum ItemType {
    Food,
    structere,
    Material
}
