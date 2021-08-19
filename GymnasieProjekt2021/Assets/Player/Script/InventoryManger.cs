using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManger : MonoBehaviour
{
    public ScriptableObject testItem;
    public GameObject BasicItem;
    public GameObject Inventory;
    public GameObject InventorySlotPanel;
    public GameObject ItemUIPrefab;
    public int xDim, yDim;

    private List<slot> Slots = new List<slot>();
    public void Start()
    {
        PlayerInputEventManager input = FindObjectOfType<PlayerInputEventManager>();
        input.inventoryKey += OpenInventory;
        input.debugSpawnItemKey += spawnTestItem;
        int i = 0;
        for(int y = 0; y < yDim; y++)
        {
            for(int x = 0; x < xDim; x++)
            {
                GameObject g = Instantiate(InventorySlotPanel.transform.GetChild(0).gameObject, new Vector3(InventorySlotPanel.transform.GetChild(0).position.x + (x * 70), InventorySlotPanel.transform.GetChild(0).position.y + (y * -65), InventorySlotPanel.transform.GetChild(0).position.z), Quaternion.identity, InventorySlotPanel.transform);
                slot t = new slot();
                t.slotGameObject = g;
                t.slotNum = i;
                Slots.Add(t);
                i++;
            } 
        }
        Destroy(InventorySlotPanel.transform.GetChild(0).gameObject);
    }

    public void changeSlot(int fromSlotNum , int toSlotNum)
    {
        if(fromSlotNum == toSlotNum || Slots[toSlotNum].isTaken)
        {
            Slots[fromSlotNum].ImgObject.transform.position = Slots[fromSlotNum].slotGameObject.transform.position;
        }
        else
        {
            setslot(toSlotNum, Slots[fromSlotNum]);
            clearSlot(fromSlotNum);
        }
    }
    public void clearSlot(int slotnum)
    {
        Slots[slotnum].amount = 0;
        Slots[slotnum].ImgObject = null;
        Slots[slotnum].isTaken = false;
    }
    public void setslot(int slotnum, slot item)
    {
        Slots[slotnum].item = item.item;
        Slots[slotnum].isTaken = true;
        Slots[slotnum].ImgObject = item.ImgObject;
        Slots[slotnum].ImgObject.transform.SetParent(Slots[slotnum].slotGameObject.transform);
        Slots[slotnum].ImgObject.transform.position = Slots[slotnum].slotGameObject.transform.position;
        Slots[slotnum].amount = item.amount;
    }
    public void addItemToInvetory(Item item)
    {
        if (doItemExist(item))
        {
            Slots[getItemIndex(item)].amount += 1;
            Debug.Log(Slots[getItemIndex(item)].amount);
        }
        else
        {
            int index = findEmptySlot();
            Slots[index].isTaken = true;
            Slots[index].item = item;
            Slots[index].ImgObject = Instantiate(ItemUIPrefab, Slots[index].slotGameObject.transform.position,Quaternion.identity, Slots[index].slotGameObject.transform);
            Slots[index].ImgObject.GetComponent<DragInventory>().canvas = Inventory.transform.parent.GetComponent<Canvas>();
            Slots[index].ImgObject.GetComponent<DragInventory>().invtory = this;
            Slots[index].ImgObject.GetComponent<Image>().sprite = Slots[index].item.Sprite;
            Slots[index].amount = 1;
        }
    }
    public int getItemIndex(Item item)
    {
        for(int i = 0; i < Slots.Count; i++)
        {
            if (Slots[i].item == item) return i;
        }
        return -1;
    }
    public bool doItemExist(Item item)
    {
        for(int i = 0; i < Slots.Count; i++)
        {
            if(Slots[i].item == item)
            {
                return true;
            }
        }
        return false;
    }
    public int findEmptySlot()
    {
        for(int i = 0; i < Slots.Count; i++)
        {
            if (!Slots[i].isTaken) return i;
        }
        return -1;
    }
    public int findNerestSlot(Vector3 pos)
    {
        Transform bestTarget = null;
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 currentPosition = pos;
        List<Transform> slotTransforms = new List<Transform>();
        foreach(slot s in Slots)
        {
            slotTransforms.Add(s.slotGameObject.transform);
        }
        foreach (Transform potentialTarget in slotTransforms)
        {
            Vector3 directionToTarget = potentialTarget.position - currentPosition;
            float dSqrToTarget = directionToTarget.sqrMagnitude;
            if (dSqrToTarget < closestDistanceSqr)
            {
                closestDistanceSqr = dSqrToTarget;
                bestTarget = potentialTarget;
            }
        }
        for(int i = 0; i < Slots.Count; i++)
        {
            if(Slots[i].slotGameObject.transform == bestTarget)
            {
                return i;
            }
        }
        return -1;
    }
    void OpenInventory()
    {
        if (Inventory.activeSelf)
        {
            Inventory.SetActive(false);
            MovmentStates.States = MovementState.walking;
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            Inventory.SetActive(true);
            MovmentStates.States = MovementState.off;
            Cursor.lockState = CursorLockMode.None;
        }
    }
    void spawnTestItem()
    {
        Debug.Log("spawned Item");
        RaycastHit ray =  Build.ViewRay(Layers.ground);
        GameObject g =  Instantiate(BasicItem, new Vector3(ray.point.x , 1 , ray.point.z) ,Quaternion.identity);
        g.AddComponent(typeof(ItemGame));
        g.GetComponent<ItemGame>().Item = testItem;
    }
}

public class slot
{
    public Item item;
    public GameObject slotGameObject;
    public GameObject ImgObject;
    public bool isTaken;
    public int amount;
    public int slotNum;
}
