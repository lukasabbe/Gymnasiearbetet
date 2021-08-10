using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new structure", menuName = "structure")]
public class StructureObject : ScriptableObject
{
    public int id = -1;
    public GameObject gameObject;

    public List<StructureVariants> structureVariants = new List<StructureVariants>();

    [System.Serializable]
    public class StructureVariants{
        public Vector3 offset = Vector3.zero;
        public Vector3 rotation = Vector3.zero;
        [Space]
        public StructureObject structure = null;
    }
}
