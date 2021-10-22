using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StuctureFinder : MonoBehaviour
{
    private List<StructureBasePlateUI> OpenInventorys = new List<StructureBasePlateUI>();
    public GameObject Structer1 = null;
    public GameObject Structer2 = null;

    private void Start()
    {
        PlayerInputEventManager input = FindObjectOfType<PlayerInputEventManager>();
        input.openStructKey += openStructure;
        input.pickUpItemKey += pickupItems;
        input.connectWiresKey += connectWire;
    }
    void pickupItems()
    {
        if(Physics.SphereCast(GameManager.playerCamera.transform.position, 0.3f, GameManager.playerCamera.transform.forward, out RaycastHit ray , 3 , Layers.item))
        {
            ray.transform.GetComponent<ItemGame>().pickUpItem(GameManager.player);
        }
    }

    void connectWire()
    {
        if (Physics.SphereCast(GameManager.playerCamera.transform.position, 0.3f, GameManager.playerCamera.transform.forward, out RaycastHit ray, 3, Layers.structure))
        {
            if(Structer1 == null)
            {
                Structer1 = ray.transform.gameObject;
            }
            else
            {
                Structer2 = ray.transform.gameObject;
            }
        }
        if(Structer1 != null && Structer2 != null)
        {
            Structer1.transform.GetComponent<ElectricSystem>().connectWire(Structer2.transform.GetComponent<ElectricSystem>().electricGroupIndex);
            Structer1 = null;
            Structer2 = null;
        }
    }

    void openStructure()
    {
        Physics.Raycast(GameManager.playerCamera.transform.position, GameManager.playerCamera.transform.forward, out RaycastHit ray, 3, Layers.structure);
        if (ray.point != Vector3.zero)
        {
            bool open = ray.transform.gameObject.GetComponent<StructureBasePlateUI>().openInventory(ray.transform.gameObject);
            if (open) OpenInventorys.Add(ray.transform.gameObject.GetComponent<StructureBasePlateUI>());
            else OpenInventorys.Clear();
        }
        else
        {
            for(int i = 0; i < OpenInventorys.Count; i++)
            {
                OpenInventorys[i].openInventory(OpenInventorys[i].gameObject);
            }
            OpenInventorys.Clear();
        }
    }
}
