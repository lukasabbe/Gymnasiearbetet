using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    public Structures structures;

    Vector3 point, target;
    public new Transform camera;
    public LayerMask groundLayer;

    public GameObject testingPrefab;
    void Update(){
        Physics.Raycast(camera.position, camera.forward, out RaycastHit ray, 10f, groundLayer);
        point = ray.point;
        target = point + (ray.normal * 0.5f);

        if (Input.GetMouseButtonDown(0)){
            //GridManager.Cell cell = GridManager.GetCell(PointToGrid(target));
            GridManager.BuildStructure(PointToGrid(target), structures.structures[0]);
        }

        if (Input.GetMouseButtonDown(0)){
            Instantiate(testingPrefab, GridManager.GetChunk(target).position, Quaternion.identity);
        }
    }
    Vector3 PointToGrid(Vector3 point){
        Vector3 pointToGrid;
        pointToGrid.x = Mathf.Round(point.x);
        pointToGrid.y = Mathf.Round(point.y);
        pointToGrid.z = Mathf.Round(point.z);

        return pointToGrid;
    }
    private void OnDrawGizmos(){
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(point, 0.2f);
        Gizmos.DrawWireCube(PointToGrid(target), Vector3.one);
    }
}
