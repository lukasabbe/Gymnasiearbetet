using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Structure", menuName = "ScriptableObjects/New structure")]
public class StructureObject : ScriptableObject
{
    public int id = -1;
    public GameObject gameObject;
}
