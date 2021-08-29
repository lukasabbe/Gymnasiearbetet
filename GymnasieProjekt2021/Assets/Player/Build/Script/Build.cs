
using UnityEngine;

public class Build : MonoBehaviour{

    static Build build;

    public StructureObject structure;

    RaycastHit buildRay, removeRay;

    public float maxBuildAngle = 0f;
    public float maxBuildDistance = 0f;

    public float rotationMultiplier = 5f;
    float rotationDelta = 0f;

    Vector3 buildPosition = new Vector3();
    Quaternion buildRotation = new Quaternion();

    public Material validMaterial, invalidMaterial;

    InventoryManager inventoryManager;

    void OnEnable() 
    {
        PlayerInputEventManager input = FindObjectOfType<PlayerInputEventManager>();

        input.leftMouseButton += OnLeftClick;
        input.rightMouseButton += OnRightClick;
        input.scroll += OnScrollDelta;
    }
    void OnDisable()
    {
        PlayerInputEventManager input = FindObjectOfType<PlayerInputEventManager>();

        input.leftMouseButton -= OnLeftClick;
        input.rightMouseButton -= OnRightClick;
        input.scroll -= OnScrollDelta;
    }
    private void Start(){
        build = this;
        inventoryManager = FindObjectOfType<InventoryManager>();
    }
    private void Update(){
        buildRay = PlayerHelperFunctions.ViewRay(buildRay, maxBuildDistance, Layers.ground);

        buildPosition = GetInstantiatePoint(buildRay, structure);
        buildRotation = Quaternion.Euler(0, rotationDelta, 0); 

        StructurePreview.ShowPreview(buildRay, structure, buildPosition, buildRotation, BuildConditions.ValidPosition(GetInstantiatePoint(buildRay, structure), structure, buildRotation), BuildConditions.ValidAngle(buildRay, maxBuildAngle), validMaterial, invalidMaterial);
    }

    void OnLeftClick(){
        if (buildRay.point == Vector3.zero) return;
        if (!BuildConditions.ValidPosition(GetInstantiatePoint(buildRay, structure), structure, buildRotation) || !BuildConditions.ValidAngle(buildRay, maxBuildAngle)) return;

        BuildStructure(buildRay, structure);
    }

    void OnRightClick(){
        removeRay = PlayerHelperFunctions.ViewRay(removeRay, maxBuildDistance, Layers.structure);

        if (removeRay.point == Vector3.zero) return;
        RemoveStructure(removeRay);
    }
  
    void OnScrollDelta(){
        rotationDelta += rotationMultiplier * Input.mouseScrollDelta.y;
    }

    Vector3 GetInstantiatePoint(RaycastHit _ray, StructureObject _structure){
        return _ray.point + (Vector3.up * _structure.Dimensions.y / 2);
    }

    void BuildStructure(RaycastHit _ray , StructureObject _structure){
        Vector3 instantiationPoint = GetInstantiatePoint(_ray, _structure);
        Instantiate(_structure.gameObject, instantiationPoint, buildRotation);

        if (GridInfo.GetChunk(buildPosition) != null) GridInfo.GetChunk(buildPosition).structures.Add(_structure);
    }

    void RemoveStructure(RaycastHit _ray){
        Destroy(_ray.transform.gameObject);
    }
}
