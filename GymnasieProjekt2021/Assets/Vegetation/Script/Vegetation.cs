using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class Vegetation : MonoBehaviour{

    public bool DebugVegetationChunks;

    [Range(0, 1)]
    public float cutoff;

    [Space]

    public float rndOffset;

    [Space]

    public float rndRotation;

    [Space]

    public float noiseZoom;
    public float noiseXOffset;
    public float noiseZOffset;

    [Space]

    public float spawnChance;

    [Space]

    public GameObject vegetation_tree_holder;
    GameObject active_vegetation_tree_holder;
    public GameObject vegetation_tree;

    [Space]

    GridInfo.Chunk[] previewChunks;

    private void OnValidate() { 

        previewChunks = GetGrownChunks(cutoff);
    }

    public void GenerateVegetation(float _cutoff){

        active_vegetation_tree_holder = Instantiate(vegetation_tree_holder, new Vector3(0, 0, 0), Quaternion.identity);

        for(int i = 0; i < previewChunks.Length; i++){
            Instantiate(vegetation_tree, previewChunks[i].position + new Vector3(Random.Range(-rndOffset, rndOffset), -13f, Random.Range(-rndOffset, rndOffset)), Quaternion.identity * Quaternion.Euler(Random.Range(-rndRotation, rndRotation), Random.Range(-180f, 180f), Random.Range(-rndRotation, rndRotation)), active_vegetation_tree_holder.transform);
        }
    }

    public GridInfo.Chunk[] GetGrownChunks(float _cutoff){
        List<GridInfo.Chunk> chunks = new List<GridInfo.Chunk>();

        for (int x = 0; x < GridInfo.regionLength; x++){
            for (int y = 0; y < GridInfo.regionLength; y++){
                if (Mathf.PerlinNoise(x / noiseZoom + noiseXOffset, y / noiseZoom + noiseZOffset) > _cutoff && Random.Range(0f, 100f) < spawnChance){
                    chunks.Add(GridInfo.chunks[x, y]);
                }
            }
        }

        return chunks.ToArray();
    }

    private void OnDrawGizmos(){

        Gizmos.color = Color.green;

        if (previewChunks.Length > 0 && DebugVegetationChunks){
            for(int i = 0; i < previewChunks.Length; i++){

                Gizmos.DrawWireCube(previewChunks[i].position, GridInfo.chunkDimensions);
            }
        }
    }
}

[CustomEditor(typeof(Vegetation))]
public class VegetationEditor : Editor{
    public override void OnInspectorGUI(){
        DrawDefaultInspector();

        Vegetation vegetation = (Vegetation)target;

        if (GUILayout.Button("Generate Vegetation")){
            vegetation.GenerateVegetation(vegetation.cutoff);
        }
    }
}



