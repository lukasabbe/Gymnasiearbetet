using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class Vegetation : MonoBehaviour{

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

    public void GenerateVegetation(float _cutoff){

        active_vegetation_tree_holder = Instantiate(vegetation_tree_holder, new Vector3(0, 0, 0), Quaternion.identity);

        for(int x = 0; x < GridInfo.regionLength; x++){
            for (int y = 0; y < GridInfo.regionLength; y++){

                if(Mathf.PerlinNoise(x / noiseZoom + noiseXOffset, y / noiseZoom + noiseZOffset) > _cutoff && Random.Range(0f, 100f) < spawnChance){

                    Instantiate(vegetation_tree, GridInfo.chunks[x, y].position + new Vector3(Random.Range(-rndOffset, rndOffset), -12.8f, Random.Range(-rndOffset, rndOffset)), Quaternion.identity * Quaternion.Euler(Random.Range(-rndRotation, rndRotation), Random.Range(-180f, 180f), Random.Range(-rndRotation, rndRotation)), active_vegetation_tree_holder.transform);
                }
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

