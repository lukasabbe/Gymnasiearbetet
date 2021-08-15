using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GridInfoMono : MonoBehaviour {
    public bool debugChunks = true;

    private void Awake()
    {
        GridInfo.chunks = GridInfo.GenerateChunks();
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

