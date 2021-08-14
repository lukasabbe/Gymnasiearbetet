using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGame : MonoBehaviour
{
    public ScriptableObject Item;
    void Start()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            other.GetComponent<InventoryManger>().items.Add((Item)Item);
            Debug.Log(other.GetComponent<InventoryManger>().items.Count);
            Destroy(gameObject);
        }
    }
}
