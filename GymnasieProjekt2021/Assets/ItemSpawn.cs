using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawn : MonoBehaviour
{
    public GameObject ItemPrefab;
    public List<Item> spawnItemsList = new List<Item>();
    public GameObject ItemHolder;
    public int MapSpawnSize;
    public int AmountOfSpwnPoints;
    public bool hasRigedbody;
    private void Start()
    {
        List<Vector3> pointsOnMap = findPointsOnMap(AmountOfSpwnPoints);
        spawnItems(pointsOnMap);
    }
    void spawnItems(List<Vector3> Points)
    {
        for(int i = 0; i < Points.Count; i++)
        {
            GameObject g= Instantiate(ItemPrefab, Points[i] + new Vector3(0,0.5f,0), Quaternion.identity, ItemHolder.transform);
            if(hasRigedbody) g.AddComponent(typeof(Rigidbody));
            g.transform.gameObject.AddComponent(typeof(ItemGame));
            g.transform.gameObject.GetComponent<ItemGame>().Item = spawnItemsList[Random.Range(0, spawnItemsList.Count)];
        }
    }
    List<Vector3> findPointsOnMap(int points)
    {
        System.Random rnd = new System.Random();
        List<Vector3> pointsOnMap = new List<Vector3>();
        for(int i = 0; i < points; i++)
        {
            if (Physics.Raycast(new Vector3(gameObject.transform.position.x + rnd.Next(-MapSpawnSize, MapSpawnSize), gameObject.transform.position.y, gameObject.transform.position.z + rnd.Next(-MapSpawnSize, MapSpawnSize)), -gameObject.transform.transform.up, out RaycastHit hit, 100, Layers.ground)) 
            {
                pointsOnMap.Add(hit.point);
            };
        }
        return pointsOnMap;
    }
}
