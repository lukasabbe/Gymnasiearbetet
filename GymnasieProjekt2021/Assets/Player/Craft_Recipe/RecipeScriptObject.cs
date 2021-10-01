using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Recipe", menuName ="ScriptableObjects/New recipe")]
public class RecipeScriptObject : ScriptableObject
{
    public List<Item> neededItems = new List<Item>();
    public List<int> amount = new List<int>();
    public Item outPutItem;
}
