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
    public int ofset;
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
        input.dropKey += dropItem;
        StartCoroutine(StartUI());
    }
    IEnumerator StartUI()
    {
        yield return new WaitForEndOfFrame();
        Debug.Log(InventorySlotPanel.GetComponent<RectTransform>().rect.width);
        int i = 0;
        for (int y = 0; y < yDim; y++)
        {
            for (int x = 0; x < xDim; x++)
            {
                GameObject g = Instantiate(InventorySlotPanel.transform.GetChild(0).gameObject);
                g.transform.SetParent(InventorySlotPanel.transform,false);
                g.transform.localPosition += new Vector3((x*60), (y*-60) , 0);
                //GameObject g = Instantiate(InventorySlotPanel.transform.GetChild(0).gameObject, new Vector3((((InventorySlotPanel.GetComponent<RectTransform>().rect.width/ xDim) - ofset) * x) + InventorySlotPanel.transform.GetChild(0).position.x, InventorySlotPanel.transform.GetChild(0).position.y + (y * -80), InventorySlotPanel.transform.GetChild(0).position.z), Quaternion.identity, InventorySlotPanel.transform);
                slot t = new slot();
                t.slotGameObject = g;
                t.slotNum = i;
                t.isTaken = false;
                Slots.Add(t);
                i++;
            }
        }
        for (int h = 0; h < HotBar.transform.childCount; h++)
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
    public void removeitem(Item item, int amount)
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
                ItemInfo.transform.GetChild(2).GetComponent<Text>().text = LatestOpenInventoryStructure[indexOfDisplaySlot - Slots.Count].amount.ToString();
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
                if(fromSlotNum < Slots.Count - 1)
                {
                    if (Slots[toSlotNum].item == Slots[fromSlotNum].item && fromSlotNum != toSlotNum)
                    {
                        setslot(toSlotNum, Slots[fromSlotNum], true);
                        removeSlot(fromSlotNum);
                    }
                    else
                    {
                        Slots[fromSlotNum].ImgObject.transform.position = Slots[fromSlotNum].slotGameObject.transform.position;
                    }
                }
                else
                {
                    if (Slots[toSlotNum].item == LatestOpenInventoryStructure[fromSlotNum-Slots.Count].item && fromSlotNum != toSlotNum)
                    {
                        setslot(toSlotNum, LatestOpenInventoryStructure[fromSlotNum - Slots.Count], true);
                        removeSlot(fromSlotNum);
                    }
                    else
                    {
                        LatestOpenInventoryStructure[fromSlotNum - Slots.Count].ImgObject.transform.position = LatestOpenInventoryStructure[fromSlotNum - Slots.Count].slotGameObject.transform.position;
                    }
                }
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
                if (fromSlotNum > Slots.Count - 1)
                {
                    if (LatestOpenInventoryStructure[toSlotNum - Slots.Count].item == LatestOpenInventoryStructure[fromSlotNum - Slots.Count].item && fromSlotNum != toSlotNum)
                    {
                        setslot(toSlotNum, LatestOpenInventoryStructure[fromSlotNum - Slots.Count], true);
                        removeSlot(fromSlotNum);
                    }
                    else
                    {
                        LatestOpenInventoryStructure[fromSlotNum - Slots.Count].ImgObject.transform.position = LatestOpenInventoryStructure[fromSlotNum - Slots.Count].slotGameObject.transform.position;
                    }
                }
                else
                {
                    if (LatestOpenInventoryStructure[toSlotNum - Slots.Count].item == Slots[fromSlotNum].item && fromSlotNum != toSlotNum)
                    {
                        setslot(toSlotNum, Slots[fromSlotNum], true);
                        removeSlot(fromSlotNum);
                    }
                    else
                    {
                        Slots[fromSlotNum].ImgObject.transform.position = Slots[fromSlotNum].slotGameObject.transform.position;
                    }
                }
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
    public void removeSlot(int slotnum)
    {
        if (slotnum > Slots.Count - 1)
        {
            slotnum -= Slots.Count;
            LatestOpenInventoryStructure[slotnum].amount = 0;
            LatestOpenInventoryStructure[slotnum].isTaken = false;
            LatestOpenInventoryStructure[slotnum].item = null;
            Destroy(LatestOpenInventoryStructure[slotnum].ImgObject);
        }
        else
        {
            Slots[slotnum].amount = 0;
            Slots[slotnum].isTaken = false;
            Slots[slotnum].item = null;
            Destroy(Slots[slotnum].ImgObject);
        }
    }
    public void setslot(int slotnum, slot item, bool addAmount = false)
    {
        if (item.ImgObject == null) Debug.Log("NULL");
        if(slotnum > Slots.Count -1)
        {
            if (addAmount)
            {
                LatestOpenInventoryStructure[slotnum - Slots.Count].amount++;
            }
            else
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
        }
        else
        {
            if (addAmount)
            {
                Slots[slotnum].amount++;
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
    }
    public void addItemToInvetory(Item item, bool inStructureInvetory, bool forceStructureInventory,int index = -1, List<slot> saveStructureInventory = null)
    {
        if (inStructureInvetory)
        {
            if (doItemExist(item , forceStructureInventory, saveStructureInventory))
            {
                Debug.Log("yes in here");
                if (saveStructureInventory == null) Slots[getItemIndex(item)].amount += 1;
                else saveStructureInventory[getItemIndex(item, saveStructureInventory)].amount += 1;
            }
            else
            {
                if (saveStructureInventory == null)
                {
                    if (index == -1) index = findEmptySlot(true);
                    LatestOpenInventoryStructure[index].isTaken = true;
                    LatestOpenInventoryStructure[index].item = item;
                    LatestOpenInventoryStructure[index].ImgObject = Instantiate(ItemUIPrefab, LatestOpenInventoryStructure[index].slotGameObject.transform.position, Quaternion.identity, LatestOpenInventoryStructure[index].slotGameObject.transform);
                    LatestOpenInventoryStructure[index].ImgObject.GetComponent<DragInventory>().canvas = LatestOpenInventoryStructure[index].ImgObject.transform.parent.parent.parent.GetComponent<Canvas>();
                    LatestOpenInventoryStructure[index].ImgObject.GetComponent<DragInventory>().invtory = this;
                    LatestOpenInventoryStructure[index].ImgObject.GetComponent<DragInventory>().startPos = index + Slots.Count;
                    LatestOpenInventoryStructure[index].ImgObject.GetComponent<Image>().sprite = LatestOpenInventoryStructure[index].item.Sprite;
                    LatestOpenInventoryStructure[index].amount = 1;
                }
                else
                {
                    if (index == -1) index = findEmptySlot(true);
                    saveStructureInventory[index].isTaken = true;
                    saveStructureInventory[index].item = item;
                    saveStructureInventory[index].ImgObject = Instantiate(ItemUIPrefab, saveStructureInventory[index].slotGameObject.transform.position, Quaternion.identity, saveStructureInventory[index].slotGameObject.transform);
                    saveStructureInventory[index].ImgObject.GetComponent<DragInventory>().canvas = saveStructureInventory[index].ImgObject.transform.parent.parent.parent.GetComponent<Canvas>();
                    saveStructureInventory[index].ImgObject.GetComponent<DragInventory>().invtory = this;
                    saveStructureInventory[index].ImgObject.GetComponent<DragInventory>().startPos = index + Slots.Count;
                    saveStructureInventory[index].ImgObject.GetComponent<Image>().sprite = saveStructureInventory[index].item.Sprite;
                    saveStructureInventory[index].amount = 1;
                }
            }
        }
        else
        {
            if (doItemExist(item, false))
            {
                Slots[getItemIndex(item)].amount += 1;
            }
            else
            {
                if (index == -1) index = findEmptySlot(false);
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
    public int getItemIndex(Item item, List<slot> saveStructureInventory = null)
    {
        if(saveStructureInventory == null)
        {
            for(int i = 0; i < Slots.Count; i++)
            {
                if (Slots[i].item == item) return i;
            }
        }
        else
        {
            for (int i = 0; i < saveStructureInventory.Count; i++)
            {
                if (saveStructureInventory[i].item == item) return i;
            }
        }
        
        return -1;
    }
    public bool doItemExist(Item item, bool forceStructureInventory, List<slot> saveStructureInventory = null)
    {
        if (!forceStructureInventory)
        {
            for(int i = 0; i < Slots.Count; i++)
            {
                if(Slots[i].item == item)
                {
                    return true;
                }
            }
        }
        else if (saveStructureInventory != null)
        {
            for (int i = 0; i < saveStructureInventory.Count; i++)
            {
                if (saveStructureInventory[i].item == item)
                {
                    return true;
                }
            }
        }
        else if(LatestOpenInventoryStructure != null)
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
    public int findEmptySlot(bool inStructureInvetory, List<slot> saveStructureInventory = null)
    {
        if(saveStructureInventory != null) 
        {
            for (int i = 0; i < LatestOpenInventoryStructure.Count; i++)
            {
                if (!saveStructureInventory[i].isTaken) return i;
            }
        }
        else if (inStructureInvetory)
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
    void dropItem()
    {
        if (!Slots[hotBarIndex].isTaken) return;
        GameObject g =  Instantiate(BasicItem, transform.position+ new Vector3(0 , 1.5f , 0) + (transform.forward * 2) , transform.rotation);
        g.AddComponent(typeof(Rigidbody));
        g.transform.gameObject.AddComponent(typeof(ItemGame));
        g.transform.gameObject.GetComponent<ItemGame>().Item = Slots[hotBarIndex].item;
        Rigidbody gRigidbody = g.GetComponent<Rigidbody>();
        gRigidbody.AddForce(transform.forward * 3, ForceMode.Impulse);
        gRigidbody.interpolation = RigidbodyInterpolation.Extrapolate;
        removeitem(Slots[hotBarIndex].item, 1);
    }
    void addToInveotry()
    {
        for(int i = 0; i < debugItems.Count; i++)
        {
            addItemToInvetory(debugItems[i], false, false);
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
