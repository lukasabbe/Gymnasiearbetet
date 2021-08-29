using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public List<Item> debugItems = new List<Item>();
    public ScriptableObject testItem;
    [Header("Public GameObjects")]
    public GameObject BasicItem;
    public GameObject Inventory;
    public GameObject InventorySlotPanel;
    public GameObject ItemUIPrefab;
    public GameObject HotBar;
    public GameObject ItemInfo;
    [Header("Setings of hotbar")]
    public int hotBarIndex;
    public int xDim, yDim;
    [HideInInspector]
    public List<slot> LatestOpenInventoryStructure;
    //private vars
    public List<slot> Slots = new List<slot>();
    [HideInInspector]
    public int firstHotBarIndex = 0;

    HotbarHandler hotbarHandler;
    public void Start()
    {
        PlayerInputEventManager input = FindObjectOfType<PlayerInputEventManager>();
        input.inventoryKey += OpenInventory;
        input.debugSpawnItemKey += addToInveotry;
        input.onHotBarDelta += onHotBarClick;
        int i = 0;
        for(int y = 0; y < yDim; y++)
        {
            for(int x = 0; x < xDim; x++)
            {
                GameObject g = Instantiate(InventorySlotPanel.transform.GetChild(0).gameObject, new Vector3(InventorySlotPanel.transform.GetChild(0).position.x + (x * 70), InventorySlotPanel.transform.GetChild(0).position.y + (y * -65), InventorySlotPanel.transform.GetChild(0).position.z), Quaternion.identity, InventorySlotPanel.transform);
                slot t = new slot();
                t.slotGameObject = g;
                t.slotNum = i;
                t.isTaken = false;
                Slots.Add(t);
                i++;
            } 
        }
        for(int h = 0; h < HotBar.transform.childCount; h++)
        {
            if (firstHotBarIndex == 0) firstHotBarIndex = i;
            slot t = new slot();
            t.slotGameObject = HotBar.transform.GetChild(h).gameObject;
            t.slotNum = i;
            t.isTaken = false;
            Slots.Add(t);
        }   
        hotbarHandler = GetComponent<HotbarHandler>();

        setHotbarIndex(firstHotBarIndex);
        Destroy(InventorySlotPanel.transform.GetChild(0).gameObject); 
    }
    public void removeitems(Item item, int amount)
    {
        int index = getItemIndex(item);
        Debug.Log("Index " + index);
        if(index > Slots.Count)
        {
            index -= Slots.Count;
            if (amount >= LatestOpenInventoryStructure[index].amount)
            {
                
                Destroy(LatestOpenInventoryStructure[index].ImgObject);
                LatestOpenInventoryStructure[index].isTaken = false;
                LatestOpenInventoryStructure[index].item = null;
                LatestOpenInventoryStructure[index].amount = 0;
            }
            else
            {
                LatestOpenInventoryStructure[index].amount -= amount;
            }
        }
        else
        {
            if(amount >= Slots[index].amount)
            {
                Destroy(Slots[index].ImgObject);
                Slots[index].isTaken = false;
                Slots[index].item = null;
                Slots[index].amount = 0;
            }
            else
            {
                Slots[index].amount -= amount;
            }
        }
    }
    public void setItemInfo(int indexOfDisplaySlot, bool activate)
    {
        if(indexOfDisplaySlot > Slots.Count-1)
        {
            if (LatestOpenInventoryStructure[indexOfDisplaySlot - Slots.Count].isTaken != true) return;
            if (activate)
            {
                ItemInfo.transform.position = LatestOpenInventoryStructure[indexOfDisplaySlot - Slots.Count].ImgObject.transform.position;
                ItemInfo.transform.GetChild(0).GetComponent<Text>().text = LatestOpenInventoryStructure[indexOfDisplaySlot - Slots.Count].item.ItemName;
                ItemInfo.transform.GetChild(1).GetComponent<Text>().text = LatestOpenInventoryStructure[indexOfDisplaySlot - Slots.Count].item.itemType.ToString();
                ItemInfo.transform.GetChild(2).GetComponent<Text>().text = LatestOpenInventoryStructure[indexOfDisplaySlot].amount.ToString();
                ItemInfo.SetActive(activate);
            }
            else
            {
                ItemInfo.SetActive(activate);
            }
        }
        else
        {
            if (Slots[indexOfDisplaySlot].isTaken != true) return;
            if (activate)
            {
                ItemInfo.transform.position = Slots[indexOfDisplaySlot].ImgObject.transform.position;
                ItemInfo.transform.GetChild(0).GetComponent<Text>().text = Slots[indexOfDisplaySlot].item.ItemName;
                ItemInfo.transform.GetChild(1).GetComponent<Text>().text = Slots[indexOfDisplaySlot].item.itemType.ToString();
                ItemInfo.transform.GetChild(2).GetComponent<Text>().text = Slots[indexOfDisplaySlot].amount.ToString();
                ItemInfo.SetActive(activate);
            }
            else
            {
                ItemInfo.SetActive(activate);
            }
        }
    }
    public void onHotBarClick(int index)
    {
        index = firstHotBarIndex + index -1;
        setHotbarIndex(index);
    }
    public void setHotbarIndex(int index)
    {
        if (index < firstHotBarIndex) return;
        Slots[hotBarIndex].slotGameObject.GetComponent<Image>().color = Color.white;
        hotBarIndex = index;
        Slots[hotBarIndex].slotGameObject.GetComponent<Image>().color = Color.gray;
        
        hotbarHandler.OnHotbarDelta();
    }
    public bool changeSlot(int fromSlotNum , int toSlotNum)
    {
        int check;
        if (fromSlotNum < toSlotNum) check = toSlotNum;
        else check = toSlotNum;

        if (check < Slots.Count -1)
        {
            if(fromSlotNum == toSlotNum || Slots[toSlotNum].isTaken)
            {
                Slots[fromSlotNum].ImgObject.transform.position = Slots[fromSlotNum].slotGameObject.transform.position;
                return false;
            }
            else
            {
                if(fromSlotNum > Slots.Count -1)
                {
                    setslot(toSlotNum, LatestOpenInventoryStructure[fromSlotNum-Slots.Count]);
                    clearSlot(fromSlotNum);
                }
                else
                {
                    setslot(toSlotNum, Slots[fromSlotNum]);
                    clearSlot(fromSlotNum);
                }
                return true;
            }
        }
        else
        {
            if (fromSlotNum == toSlotNum || LatestOpenInventoryStructure[toSlotNum - Slots.Count].isTaken)
            {
                LatestOpenInventoryStructure[fromSlotNum - Slots.Count].ImgObject.transform.position = LatestOpenInventoryStructure[fromSlotNum - Slots.Count].slotGameObject.transform.position;
                return false;
            }
            else
            {
                if (fromSlotNum > Slots.Count -1 )
                {
                    setslot(toSlotNum, LatestOpenInventoryStructure[fromSlotNum - Slots.Count]);
                    clearSlot(fromSlotNum);
                }
                else
                {
                    setslot(toSlotNum, Slots[fromSlotNum]);
                    clearSlot(fromSlotNum);
                }
                return true;
            }
        }
    }
    public void clearSlot(int slotnum)
    {
        if(slotnum > Slots.Count - 1)
        {
            slotnum -= Slots.Count;
            LatestOpenInventoryStructure[slotnum].amount = 0;
            LatestOpenInventoryStructure[slotnum].ImgObject = null;
            LatestOpenInventoryStructure[slotnum].isTaken = false;
            LatestOpenInventoryStructure[slotnum].item = null;
        }
        else
        {
            Slots[slotnum].amount = 0;
            Slots[slotnum].ImgObject = null;
            Slots[slotnum].isTaken = false;
            Slots[slotnum].item = null;
        }
    }
    public void setslot(int slotnum, slot item)
    {
        if (item.ImgObject == null) Debug.Log("NULL");
        if(slotnum > Slots.Count -1)
        {
            slotnum -= Slots.Count;
            LatestOpenInventoryStructure[slotnum].item = item.item;
            LatestOpenInventoryStructure[slotnum].isTaken = true;
            LatestOpenInventoryStructure[slotnum].ImgObject = item.ImgObject;
            LatestOpenInventoryStructure[slotnum].ImgObject.transform.SetParent(LatestOpenInventoryStructure[slotnum].slotGameObject.transform);
            LatestOpenInventoryStructure[slotnum].ImgObject.transform.position = LatestOpenInventoryStructure[slotnum].slotGameObject.transform.position;
            LatestOpenInventoryStructure[slotnum].amount = item.amount;
            LatestOpenInventoryStructure[slotnum].ImgObject.GetComponent<DragInventory>().canvas = LatestOpenInventoryStructure[slotnum].ImgObject.transform.parent.parent.parent.GetComponent<Canvas>();
        }
        else
        {
            Slots[slotnum].item = item.item;
            Slots[slotnum].isTaken = true;
            Slots[slotnum].ImgObject = item.ImgObject;
            Slots[slotnum].ImgObject.transform.SetParent(Slots[slotnum].slotGameObject.transform);
            Slots[slotnum].ImgObject.transform.position = Slots[slotnum].slotGameObject.transform.position;
            Slots[slotnum].amount = item.amount;
            Slots[slotnum].ImgObject.GetComponent<DragInventory>().canvas = Inventory.transform.parent.GetComponent<Canvas>();
        }
    }
    public void addItemToInvetory(Item item, bool inStructureInvetory)
    {
        if (inStructureInvetory)
        {
            Debug.Log("yes2");
            if (doItemExist(item))
            {
                Debug.Log("yes3");
                Slots[getItemIndex(item)].amount += 1;
            }
            else
            {
                Debug.Log("yes4");
                int index = findEmptySlot(true);
                Debug.Log(index);
                LatestOpenInventoryStructure[index].isTaken = true;
                LatestOpenInventoryStructure[index].item = item;
                LatestOpenInventoryStructure[index].ImgObject = Instantiate(ItemUIPrefab, LatestOpenInventoryStructure[index].slotGameObject.transform.position, Quaternion.identity, LatestOpenInventoryStructure[index].slotGameObject.transform);
                LatestOpenInventoryStructure[index].ImgObject.GetComponent<DragInventory>().canvas = LatestOpenInventoryStructure[index].ImgObject.transform.parent.parent.parent.GetComponent<Canvas>();
                LatestOpenInventoryStructure[index].ImgObject.GetComponent<DragInventory>().invtory = this;
                LatestOpenInventoryStructure[index].ImgObject.GetComponent<DragInventory>().startPos = index + Slots.Count;
                LatestOpenInventoryStructure[index].ImgObject.GetComponent<Image>().sprite = LatestOpenInventoryStructure[index].item.Sprite;
                LatestOpenInventoryStructure[index].amount = 1;
            }
        }
        else
        {
            if (doItemExist(item))
            {
                Slots[getItemIndex(item)].amount += 1;
                Debug.Log(Slots[getItemIndex(item)].amount);
            }
            else
            {
                int index = findEmptySlot(false);
                Slots[index].isTaken = true;
                Slots[index].item = item;
                Slots[index].ImgObject = Instantiate(ItemUIPrefab, Slots[index].slotGameObject.transform.position,Quaternion.identity, Slots[index].slotGameObject.transform);
                Slots[index].ImgObject.GetComponent<DragInventory>().canvas = Inventory.transform.parent.GetComponent<Canvas>();
                Slots[index].ImgObject.GetComponent<DragInventory>().invtory = this;
                Slots[index].ImgObject.GetComponent<DragInventory>().startPos = index;
                Slots[index].ImgObject.GetComponent<Image>().sprite = Slots[index].item.Sprite;
                Slots[index].amount = 1;
            }
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
                Debug.Log("test");
                return true;
            }
        }
        if(LatestOpenInventoryStructure != null)
        {
            for(int i = 0; i<LatestOpenInventoryStructure.Count; i++)
            {
                if (LatestOpenInventoryStructure[i].item == item)
                {
                    return true;
                }
            }
        }
        return false;
    }
    public int findEmptySlot(bool inStructureInvetory)
    {
        if (inStructureInvetory)
        {
            for (int i = 0; i < LatestOpenInventoryStructure.Count; i++)
            {
                if (!LatestOpenInventoryStructure[i].isTaken) return i;
            }
        }
        else
        {
            for(int i = 0; i < Slots.Count; i++)
            {
                if (!Slots[i].isTaken) return i;
            }
        }
        return -1;
    }
    public int findNerestSlot(Vector3 pos)
    {
        Transform bestTarget = null;
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 currentPosition = pos;
        List<Transform> slotTransforms = new List<Transform>();
        foreach (slot s in Slots)
        {
            if(Inventory.activeSelf || s.slotNum >= firstHotBarIndex) slotTransforms.Add(s.slotGameObject.transform);
        }
        if(LatestOpenInventoryStructure != null)
        {
            foreach(slot s in LatestOpenInventoryStructure)
            {
                slotTransforms.Add(s.slotGameObject.transform);
            }
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
        for (int i = 0; i < LatestOpenInventoryStructure.Count; i++)
        {
            if (LatestOpenInventoryStructure[i].slotGameObject.transform == bestTarget)
            {
                return i + Slots.Count;
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
        GameObject g =  Instantiate(BasicItem, new Vector3(0, 3f, 0) ,Quaternion.identity);
        g.AddComponent(typeof(ItemGame));
        g.GetComponent<ItemGame>().Item = testItem;
    }
    void addToInveotry()
    {
        for(int i = 0; i < debugItems.Count; i++)
        {
            addItemToInvetory(debugItems[i], false);
        }
    }
}
public class slot
{
    public Item item = null;
    public GameObject slotGameObject;
    public GameObject ImgObject;
    public bool isTaken;
    public int amount;
    public int slotNum;
}
