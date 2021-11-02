using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGame : MonoBehaviour
{
    public ScriptableObject Item;

    public void pickUpItem(GameObject player)
    {
        player.GetComponent<InventoryManager>().addItemToInvetory((Item)Item, false,false);
        Destroy(gameObject);
    }
    /*
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            other.GetComponent<InventoryManager>().addItemToInvetory((Item)Item, false);
            Destroy(gameObject.transform.parent.gameObject);
        }
    }
   */
    
}
