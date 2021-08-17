using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Build : MonoBehaviour{
    public StructureObject structure;
    RaycastHit buildRay, removeRay;

    public float rotationMultiplier = 5f;
    float rotationDelta = 0f;

    Vector3 buildPosition = new Vector3();
    Quaternion buildRotation = new Quaternion();

    private void Start()
    {
        PlayerInputEventManager input = FindObjectOfType<PlayerInputEventManager>();

        input.leftMouseButton += OnLeftClick;
        input.rightMouseButton += OnRightClick;
        input.scroll += OnScrollDelta;
    }

    private void Update()
    {
        buildRay = ViewRay(Layers.ground);
        removeRay = ViewRay(Layers.structure);
        ShowPreview(buildRay, structure);

        buildPosition = GetInstantiatePoint(buildRay, structure);
        buildRotation = Quaternion.Euler(0, rotationDelta, 0);
    }

    void OnLeftClick()
    {
        if (MovmentStates.States == MovementState.off) return;
        if (buildRay.point == Vector3.zero) return;
        if (!ValidPosition(GetInstantiatePoint(buildRay, structure), structure) || !ValidRotation(buildRay)) return;

        BuildStructure(buildRay, structure);
    }

    void OnRightClick()
    {
        if (MovmentStates.States == MovementState.off) return;
        if (removeRay.point == Vector3.zero) return;
        RemoveStructure(removeRay);
    }
  
    void OnScrollDelta()
    {
        rotationDelta += rotationMultiplier * Input.mouseScrollDelta.y;
    }

    public static RaycastHit ViewRay(LayerMask _layer)
    {
        Physics.Raycast(GameManager.playerCamera.transform.position, GameManager.playerCamera.transform.forward, out RaycastHit ray, 10f, _layer);
        return ray;
    }

    GameObject preview = null;
    public Material validMaterial, invalidMaterial;
    void ShowPreview(RaycastHit _ray, StructureObject _structure)
    {
        if (preview == null)
        {
            preview = Instantiate(_structure.gameObject, buildPosition, buildRotation);
            preview.layer = 1 << 0;

            preview.GetComponent<Collider>().enabled = false;
        }

        if (preview.activeSelf)
        {
            if (ValidPosition(buildPosition, _structure) && ValidRotation(_ray))
            {
                preview.GetComponent<Renderer>().material = validMaterial;
            }
            else
            {
                preview.GetComponent<Renderer>().material = invalidMaterial;
            }
        }

        if (_ray.point == Vector3.zero)
        {
            preview.SetActive(false);
        }
        else
        {
            preview.SetActive(true);

            preview.transform.position = buildPosition;
            preview.transform.rotation = buildRotation;
        }
    }

    bool ValidPosition(Vector3 _instantiationPoint, StructureObject _structure)
    {
        return !Physics.CheckBox(_instantiationPoint, _structure.Dimensions / 2, buildRotation, Layers.structure);
    }

    bool ValidRotation(RaycastHit _ray)
    {
        return Vector3.Dot(transform.up, _ray.normal) > 0.95f;
    }

    Vector3 GetInstantiatePoint(RaycastHit _ray, StructureObject _structure)
    {
        return _ray.point + (Vector3.up * _structure.Dimensions.y / 2);
    }

    void BuildStructure(RaycastHit _ray , StructureObject _structure)
    {
        Vector3 instantiationPoint = GetInstantiatePoint(_ray, _structure);
        Instantiate(_structure.gameObject, instantiationPoint, buildRotation);

        GridInfo.GetChunk(buildPosition).structures.Add(_structure);
    }

    void RemoveStructure(RaycastHit _ray)
    {
        Destroy(_ray.transform.gameObject);
    }
}
