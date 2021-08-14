using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManger : MonoBehaviour
{
    public ScriptableObject testItem;
    public GameObject BasicItem;
    public List<Item> items = new List<Item>();
    public void Start()
    {
        PlayerInputEventManager input = FindObjectOfType<PlayerInputEventManager>();
        input.inventoryKey += OpenInventory;
        input.debugSpawnItemKey += spawnTestItem;
    }
    void OpenInventory()
    {
        
    }
    void spawnTestItem()
    {
        Debug.Log("spawned Item");
        RaycastHit ray =  Build.ViewRay();
        GameObject g =  Instantiate(BasicItem, new Vector3(ray.point.x , 1 , ray.point.z) ,Quaternion.identity);
        g.AddComponent(typeof(ItemGame));
        g.GetComponent<ItemGame>().Item = testItem;
    }
}
