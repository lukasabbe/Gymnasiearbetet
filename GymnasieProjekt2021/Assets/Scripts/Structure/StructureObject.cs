using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Structure", menuName = "ScriptableObjects/New structure")]
public class StructureObject : ScriptableObject
{
    [HideInInspector] public Vector3 position;
    [HideInInspector] public Vector3 rotation;
    public Vector3 Dimensions;

    public GameObject gameObject;
}
