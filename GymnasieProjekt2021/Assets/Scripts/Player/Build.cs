using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Build : MonoBehaviour{
    public Structures structures;
    private void Start()
    {
        PlayerInputEventManager input = FindObjectOfType<PlayerInputEventManager>();

        input.leftMouseButton += OnLeftClick;
        input.rightMouseButton += OnRightClick;
        input.debugKey += OnDebugKey;
    }

    void OnLeftClick()
    {
        RaycastHit ray = ViewRay();
        if (ray.point == Vector3.zero) return;

        GridInfo.Cell cell = GridInfo.GetCell(GridInfo.PointToGrid(ray.point + ray.normal.normalized * 0.5f));
        BuildStructure(cell, structures.structures[0]);
    }

    void OnRightClick()
    {
        RaycastHit ray = ViewRay();
        if (ray.point == Vector3.zero) return;

        GridInfo.Cell cell = GridInfo.GetCell(GridInfo.PointToGrid(ray.point + ray.normal.normalized * -0.5f));
        RemoveStructure(cell);
    }

    void OnDebugKey()
    {
        
    }
    public static RaycastHit ViewRay()
    {
        Physics.Raycast(GameManager.playerCamera.transform.position, GameManager.playerCamera.transform.forward, out RaycastHit ray, 10f, Layers.ground);
        return ray;
    }

    void BuildStructure(GridInfo.Cell _cell, StructureObject _structure)
    {
        if (_cell == null) return;
        if (_cell.occupationState != GridInfo.CellStates.OccupationState.empty) return;

        _cell.structure = _structure;
        _cell.gameObject = Instantiate(_structure.gameObject, _cell.position, Quaternion.identity);

        _cell.occupationState = GridInfo.CellStates.OccupationState.occupied;
    }

    void RemoveStructure(GridInfo.Cell _cell)
    {
        if (_cell == null) return;

        Vector3 _cellPosition = _cell.position;

        _cell.structure = null;
        Destroy(_cell.gameObject);

        _cell.occupationState = GridInfo.CellStates.OccupationState.empty;
    }

    public static void ReplaceStructure(GridInfo.Cell _cell, GameObject gameObject)
    {
        Destroy(_cell.gameObject);
        _cell.gameObject = Instantiate(gameObject, _cell.position, Quaternion.identity);
    }

    /*
    void Update(){
        if (Input.GetMouseButtonDown(0)){
            Build(ViewTarget());
            GridManager.Chunk chunk = GridManager.GetChunk(ViewTarget());
            for (int i = 0; i < chunk.cells.Count; i++){
                if (chunk.cells[i].isOccupied){
                    chunk.cells[i].isUpdated = false;
                    GridManager.UpdateCellVariant(chunk.cells[i]);
                }
            }
        }
        if (Input.GetMouseButtonDown(1)){
            Remove(InverseViewTarget());
            GridManager.Chunk chunk = GridManager.GetChunk(InverseViewTarget());
            for (int i = 0; i < chunk.cells.Count; i++){
                if (chunk.cells[i].isOccupied){
                    chunk.cells[i].isUpdated = false;
                    GridManager.UpdateCellVariant(chunk.cells[i]);
                }
            }
        }
    }
    Vector3 ViewPoint(){
        Physics.Raycast(camera.position, camera.forward, out RaycastHit ray, 10f, Layers.ground);
        return ray.point;
    }
    Vector3 ViewTarget(){
        Vector3 point, target;
        Physics.Raycast(camera.position, camera.forward, out RaycastHit ray, 10f, Layers.ground);

        point = ray.point;
        if (point == Vector3.zero) return Vector3.zero;

        target = point + (ray.normal * 0.5f);

        return target;
    } 
    Vector3 InverseViewTarget(){
        Vector3 point, target;
        Physics.Raycast(camera.position, camera.forward, out RaycastHit ray, 10f, Layers.ground);

        point = ray.point;
        if (point == Vector3.zero) return Vector3.zero;

        target = point + (ray.normal * -0.5f);

        return target;
    }
    void Build(Vector3 target){
        if (target == Vector3.zero) return;
        GridManager.Cell cell = GridManager.GetCell(PointToGrid(target), GridManager.GetChunk(target));
        if (cell == null) return;
        GridManager.BuildStructure(cell, structures.structures[0]);
    }
    void Remove(Vector3 target){
        if (target == Vector3.zero) return;
        GridManager.Cell cell = GridManager.GetCell(PointToGrid(target), GridManager.GetChunk(target));
        if (cell == null) return;
        GridManager.RemoveStructure(cell);
    }
    Vector3 PointToGrid(Vector3 point){
        Vector3 pointToGrid;
        pointToGrid.x = Mathf.Round(point.x);
        pointToGrid.y = Mathf.Round(point.y);
        pointToGrid.z = Mathf.Round(point.z);

        return pointToGrid;
    }
    */ //Gammal kod 
}
