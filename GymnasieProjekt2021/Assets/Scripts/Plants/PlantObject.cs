using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Plant", menuName = "Plant")]
public class PlantObject : ScriptableObject
{
    public string plantName = "NONE";
    public string plantDescription = "NONE";

    public float lifeTime = 0f;
}
