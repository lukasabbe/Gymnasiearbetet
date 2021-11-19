using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorCode : MonoBehaviour
{
    public float electricyEfficiency;

    [HideInInspector] public StructureBasePlateUI basePlateUI;
    [HideInInspector] public ElectricSystem electric;
    private bool isGenerating;

    private void Start()
    {
        basePlateUI = GetComponent<StructureBasePlateUI>();
        electric = GetComponent<ElectricSystem>();
    }
    private void Update()
    {
        GenerateElectry();
    }

    void GenerateElectry()
    {
        if (basePlateUI.Slots[0].isTaken && !isGenerating)
        {
            if(basePlateUI.Slots[0].item.itemType == ItemType.Material)
            {
                MaterialItem materialItem = (MaterialItem)basePlateUI.Slots[0].item;
                if (materialItem.isFuel)
                {
                     isGenerating = true;
                    if(materialItem.ItemName == "coal")
                    {
                        StartCoroutine(generating(20 * electricyEfficiency));
                    }
                    else
                    {
                        StartCoroutine(generating(10 * electricyEfficiency));
                    }
                }
            }
        }
    }

    IEnumerator generating(float time)
    {
        electric.electricPerMin = 4;
        staticElectricSystem.electricGroups[electric.electricGroupIndex].hasEnergy();
        yield return new WaitForSeconds(time);
        isGenerating = false;
        basePlateUI.removeItem(0, 1);
        electric.electricPerMin = 0;
        staticElectricSystem.electricGroups[electric.electricGroupIndex].hasEnergy();
    }

}
