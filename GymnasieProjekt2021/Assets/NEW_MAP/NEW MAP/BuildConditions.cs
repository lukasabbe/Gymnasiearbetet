using UnityEngine;

public static class BuildConditions{
    public static bool ValidPosition(Vector3 _instantiationPoint, StructureObject _structure, Quaternion _buildRotation){
        return !Physics.CheckBox(_instantiationPoint, _structure.Dimensions / 2, _buildRotation, Layers.structure);
    }

    public static bool ValidAngle(RaycastHit _ray, float maxBuildAngle){
        return Vector3.Dot(new Vector3(0, 1, 0), _ray.normal) > maxBuildAngle;
    }
}
