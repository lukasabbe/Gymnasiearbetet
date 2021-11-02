using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialManager : MonoBehaviour
{
    [HideInInspector] public enum MaterialType { Wood, Rock };
    public MaterialType materialType;
    public int equipmentTeirRequired;
    public int health;
}
