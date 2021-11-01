using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class infuserStructer : MonoBehaviour
{
    //public obj
    public List<RecipeScriptObject> recepis = new List<RecipeScriptObject>();

    //private obj
    private StructureBasePlateUI basePlate;
    //vars
    public float waitTime;
    private bool fusing = false;
    private void Start()
    {
        basePlate = GetComponent<StructureBasePlateUI>();
    }
    private void Update()
    {
        fuseItem();
    }
    void fuseItem()
    {
        if (!fusing)
        {
            if(basePlate.Slots[0].isTaken && basePlate.Slots[1].isTaken)
            {
                for(int i = 0; i < recepis.Count; i++)
                {
                    if ((recepis[i].neededItems[0] == basePlate.Slots[0].item && recepis[i].neededItems[1] == basePlate.Slots[1].item) || (recepis[i].neededItems[0] == basePlate.Slots[1].item && recepis[i].neededItems[1] == basePlate.Slots[0].item))
                    {
                        StartCoroutine(fuseItemTimer(recepis[i].outPutItem));
                        fusing = true;
                    }
                }
            }
        }
    }
    IEnumerator fuseItemTimer(Item outputitem)
    {
        basePlate.removeItem(0,1);
        basePlate.removeItem(1,1);
        yield return new WaitForSeconds(waitTime);
        basePlate.PlayerInventory.addItemToInvetory(outputitem, true, true, 2, basePlate.Slots);
        fusing = false;
    }
}
