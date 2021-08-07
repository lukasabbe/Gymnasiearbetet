using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    public Structures structures;

    Vector3 point, target, inverseTarget;
    public new Transform camera;
    public LayerMask groundLayer;

    public GameObject testingPrefab;
    void Update(){
        Physics.Raycast(camera.position, camera.forward, out RaycastHit ray, 10f, groundLayer);
        point = ray.point;
        target = point + (ray.normal * 0.5f);
        inverseTarget = point + (ray.normal * -0.5f);

        if (Input.GetMouseButtonDown(0)){
            GridManager.Cell cell = GridManager.GetCell(PointToGrid(target), GridManager.GetChunk(target));
            if (cell == null) return;
            GridManager.BuildStructure(cell, structures.structures[0]);
        }
        if (Input.GetMouseButtonDown(1)){
            GridManager.Cell cell = GridManager.GetCell(PointToGrid(inverseTarget), GridManager.GetChunk(inverseTarget));
            if (cell == null) return;
            GridManager.DeconstructStructure(cell);
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            int cellsOccupied = 0;
            List<int> cellTypes = new List<int>();
            for(int i = 0; i < GridManager.GetChunk(transform.position).cells.Count; i++)
            {
                if(GridManager.GetChunk(transform.position).cells[i].occupied == true)
                {
                    cellsOccupied++;
                    cellTypes.Add(i);
                }
            }
            Debug.Log($"{cellsOccupied} cells are occupied in this chunk");
            for(int i = 0; i < cellTypes.Count; i++)
            {
                Debug.Log($"Cell {i}'s position: {GridManager.GetChunk(transform.position).cells[i].position}");
                Debug.Log($"Cell {i}'s id: {GridManager.GetChunk(transform.position).cells[i].structure.id}");
            }
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
