using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class Vegetation : MonoBehaviour{

    [Range(0, 1)]
    public float cutoff;

    public float rndOffsetX;
    public float rndOffsetZ;

    public float noiseRange;
    public float noiseXOffset;
    public float noiseZOffset;

    public float spawnChance;

    public GameObject vegetation_tree_holder;
    GameObject active_vegetation_tree_holder;
    public GameObject vegetation_tree;

    public void GenerateVegetation(float _cutoff){

        active_vegetation_tree_holder = Instantiate(vegetation_tree_holder, new Vector3(0, 0, 0), Quaternion.identity);

        for(int x = 0; x < GridInfo.regionLength; x++){
            for (int y = 0; y < GridInfo.regionLength; y++){
                if(Mathf.PerlinNoise(x / noiseRange + noiseXOffset, y / noiseRange + noiseZOffset) > _cutoff && Random.Range(0f, 100f) < spawnChance){
                    Physics.Raycast(GridInfo.chunks[x, y].position, Vector3.down, out RaycastHit ray);
                    Instantiate(vegetation_tree, ray.point + new Vector3(Random.Range(-5f, 5f), -1.2f, Random.Range(-5f, 5f)), Quaternion.identity * Quaternion.Euler(Random.Range(-5f, 5f), Random.Range(-180f, 180f), Random.Range(-5f, 5f)), active_vegetation_tree_holder.transform);
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

