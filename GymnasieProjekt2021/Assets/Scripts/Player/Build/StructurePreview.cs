using UnityEngine;

public static class StructurePreview{

    static GameObject preview = null;

    public static void ShowPreview(RaycastHit _ray, StructureObject _structure, Vector3 _buildPosition, Quaternion _buildRotation, bool _validPosition, bool _validRotation, Material _validMaterial, Material _invalidMaterial){

        if (preview == null){
            preview = GameObject.Instantiate(_structure.gameObject, _buildPosition, _buildRotation);
            preview.layer = 1 << 0;

            preview.GetComponent<Collider>().enabled = false;
        }

        bool isActive = _ray.point == Vector3.zero ? false : true;
        if (isActive )


        if (preview.activeSelf){
            preview.transform.position = _buildPosition;
            preview.transform.rotation = _buildRotation;

            if (_validPosition && _validRotation) preview.GetComponent<Renderer>().material = _validMaterial;
            else preview.GetComponent<Renderer>().material = _invalidMaterial;
        }
    }
}
