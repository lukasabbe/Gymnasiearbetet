using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorCode : MonoBehaviour
{
    public StructureBasePlateUI basePlateUI;

    private void Start()
    {
        basePlateUI = GetComponent<StructureBasePlateUI>();
    }
    private void Update()
    {
        GenerateElectry();
    }

    void GenerateElectry()
    {
        if (basePlateUI.Slots[0].isTaken)
        {
            if(basePlateUI.Slots[0].item.itemType == ItemType.Material)
            {
                MaterialItem materialItem = (MaterialItem)basePlateUI.Slots[0].item;
                if (materialItem.isFuel)
                {

                }
            }
        }
    }

    IEnumerator generating()
    {
        yield return new WaitForEndOfFrame();
    }

}
