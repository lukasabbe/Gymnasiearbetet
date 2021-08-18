using UnityEngine;

public class Build : MonoBehaviour{

    static Build build;

    [Header("Remove structure when hotbar is implemented. Only for testing purpouses")]
    public StructureObject structure;

    RaycastHit buildRay, removeRay;

    [Range(0, 1)]
    public float maxBuildAngle = 0f;
    public float maxBuildDistance = 0f;

    [Space]
    [Header("How much the structure should rotate when OnScrollDelta() is called")]
    public float rotationMultiplier = 5f;
    float rotationDelta = 0f;

    Vector3 buildPosition = new Vector3();
    Quaternion buildRotation = new Quaternion();

    [Space]

    public Material validMaterial, invalidMaterial;

    private void Start(){
        build = this;

        PlayerInputEventManager input = FindObjectOfType<PlayerInputEventManager>();

        input.leftMouseButton += OnLeftClick;
        input.rightMouseButton += OnRightClick;
        input.scroll += OnScrollDelta;
    }

    private void Update(){
        buildRay = ViewRay(Layers.ground);

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
        removeRay = ViewRay(Layers.structure);

        if (removeRay.point == Vector3.zero) return;
        RemoveStructure(removeRay);
    }
  
    void OnScrollDelta(){
        rotationDelta += rotationMultiplier * Input.mouseScrollDelta.y;
    }

    public static RaycastHit ViewRay(LayerMask _layer){
        Physics.Raycast(GameManager.playerCamera.transform.position, GameManager.playerCamera.transform.forward, out RaycastHit ray, build.maxBuildDistance, _layer);
        return ray;
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

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(removeRay.point, 0.2f);
    }
}
