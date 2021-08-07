using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManagerMono : MonoBehaviour
{
    public Transform player;
    public int gridLength = 8, gridHeight = 8, chunkLength = 3;
    private void Awake(){
        GridManager.InitializeChunks(chunkLength, gridLength, gridHeight);
    }
    private void OnDrawGizmos(){
        Gizmos.color = Color.red;
        for(int i = 0; i < GridManager.chunks.Count; i++){
            Gizmos.DrawWireCube(GridManager.chunks[i].position, GridManager.chunks[i].size);
        }

        Gizmos.color = Color.white;
        Gizmos.DrawWireCube(GridManager.GetChunk(player.position).position, GridManager.GetChunk(player.position).size);
        for(int i = 0; i < GridManager.GetChunk(player.position).cells.Count; i++){
            Gizmos.DrawWireCube(GridManager.GetChunk(player.position).cells[i].position, Vector3.one);
        }
    }
}
