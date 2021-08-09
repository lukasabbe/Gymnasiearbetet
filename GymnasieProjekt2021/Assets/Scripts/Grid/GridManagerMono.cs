using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManagerMono : MonoBehaviour {
    public int gridLength = 8, gridHeight = 8, chunkLength = 3;
    private void Awake() {
        GridManager.chunkLength = chunkLength;
        GridManager.gridLength = gridLength;
        GridManager.gridHeight = gridHeight;

        GridManager.InitializeChunks(chunkLength, gridLength, gridHeight);

        GridManager.LoadChunk(GridManager.GetChunk(GameManager.player.transform.position));
    }
    private void FixedUpdate(){
        GridManager.LoadChunks(GridManager.GetSorroundingChunks(GameManager.player.transform.position));
    }
    private void OnDrawGizmos() {
        if (Application.isPlaying){
            Gizmos.color = Color.red;

            for (int i = 0; i < GridManager.chunks.Count; i++){
                if (GridManager.chunks[i].isLoaded)
                    Gizmos.color = Color.yellow;
                else
                    Gizmos.color = Color.red;
                Gizmos.DrawWireCube(GridManager.chunks[i].position, GridManager.chunks[i].size);
            }

            /*

            GridManager.Chunk[] chunks = GridManager.GetSorroundingChunks(GameManager.player.transform.position);
            for (int i = 0; i < chunks.Length; i++)
            {
                Gizmos.DrawWireCube(chunks[i].position, chunks[i].size);
            }

            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(GridManager.GetChunk(GameManager.player.transform.position).position, GridManager.GetChunk(GameManager.player.transform.position).size);
            */
        }
    }
}

