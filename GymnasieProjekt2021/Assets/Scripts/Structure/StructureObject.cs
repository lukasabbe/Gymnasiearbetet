using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new structure", menuName = "structure")]
public class StructureObject : ScriptableObject
{
    public int id = -1;
    public GameObject gameObject;
}
