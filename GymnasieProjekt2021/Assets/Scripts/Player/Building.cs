using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour{
    public Structures structures;

    public new Transform camera;

    bool displayChunk = false;
    void Update(){
        if (Input.GetMouseButtonDown(0)) Build(ViewTarget());
        if (Input.GetMouseButtonDown(1)) Remove(InverseViewTarget());
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
}
