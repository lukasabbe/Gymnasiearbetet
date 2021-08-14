using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GridInfoMono : MonoBehaviour {
    public bool debugChunks = true;
    public bool debugCells = true;

    public event Action onChunkEntered;
    GridInfo.Chunk currentChunk;

    private void Awake()
    {
        GridInfo.chunks = GridInfo.GenerateChunks();
        currentChunk = GridInfo.GetChunk(GameManager.player.transform.position);

        InitializeChunks();
    }

    private void Update()
    {
        
    }

    void InitializeChunks() // Ger alla chunks celler
    {
        for(int x = 0; x < GridInfo.regionLength; x++)
        {
            for (int y = 0; y < GridInfo.regionLength; y++)
            {
                GridInfo.GenerateCells(GridInfo.chunks[x, y]);
            }
        }
    }
    private void OnDrawGizmos()
    {
        if (Application.isPlaying)
        {
            if (debugChunks)
            {
                Gizmos.color = Color.red;
                for (int x = 0; x < GridInfo.regionLength; x++)
                {
                    for (int z = 0; z < GridInfo.regionLength; z++)
                    {
                        Gizmos.DrawWireCube(GridInfo.chunks[x, z].position, GridInfo.chunks[x, z].dimensions);
                    }
                }

                GridInfo.Chunk[] sorroundingChunks = GridInfo.GetNeighbouringChunks(GameManager.player.transform.position);
                if (sorroundingChunks != null)
                {
                    Gizmos.color = Color.yellow;
                    for (int i = 0; i < sorroundingChunks.Length; i++)
                    {
                        if (sorroundingChunks[i] != null) Gizmos.DrawWireCube(sorroundingChunks[i].position, sorroundingChunks[i].dimensions);
                    }
                }

                GridInfo.Chunk currentChunk = GridInfo.GetChunk(GameManager.player.transform.position);
                if (currentChunk != null)
                {
                    Gizmos.color = Color.green;
                    Gizmos.DrawWireCube(currentChunk.position, new Vector3(GridInfo.chunkLength, GridInfo.chunkHeight, GridInfo.chunkLength));
                }
            }
        }
    }
}

