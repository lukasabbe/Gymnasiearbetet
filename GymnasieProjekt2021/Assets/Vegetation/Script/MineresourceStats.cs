using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineresourceStats : MonoBehaviour
{
    public GameObject BasicItem;
    public Item itemInRecurece;
    public int amount;
    private GameObject ItemsParent;
    public void Start()
    {
        ItemsParent = GameObject.Find("ItemsOnGround");
    }
    public void DestroyRecurece()
    {
        for(int i = 0; i < amount; i++)
        {
            GameObject g = Instantiate(BasicItem, new Vector3(transform.position.x + Random.Range(-0.3f, 0.3f), transform.position.y, transform.position.z + Random.Range(-0.3f, 0.3f)), Quaternion.identity, ItemsParent.transform);
            g.AddComponent(typeof(Rigidbody));
            g.transform.gameObject.AddComponent(typeof(ItemGame));
            g.transform.gameObject.GetComponent<ItemGame>().Item = itemInRecurece;
        }

        Destroy(gameObject);
    }
}
