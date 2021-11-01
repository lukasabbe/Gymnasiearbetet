using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SmelterStaion : MonoBehaviour
{
    public List<RecipeScriptObject> recipes = new List<RecipeScriptObject>();
    public List<RecipeScriptObject> CombinedRecepice = new List<RecipeScriptObject>();
    //GameObjects
    public GameObject isSmelterOnBtn;
    public Slider timerSlider;
    public Text fuelAmountText;
    public GameObject modeBtn;
    public Text modeText;
    //scripts
    private StructureBasePlateUI basePlate;
    //vars
    public bool isOn = true;
    public float waitTime;
    private bool startedSmelting = true;
    public bool useFuel;
    public bool useEnergy;
    private float fuleAmount;
    private int modeNum = 0;

    ElectricSystem el;

    private void Start()
    {
        basePlate = GetComponent<StructureBasePlateUI>();
        isSmelterOnBtn.GetComponent<Button>().onClick.AddListener(turnOnAndOf);
        el = GetComponent<ElectricSystem>();
        if (useEnergy)
        {
            modeBtn.GetComponent<Button>().onClick.AddListener(changeMode);
            uppdateModeText();
        }
    }
    private void Update()
    {
        if (isOn)
        {
            if (useEnergy)
            {
                if(modeNum == 0)
                {
                    smeltItem();
                }
                else
                {
                    combinedItem();
                }
            }
            else
            {
                smeltItem();
            }
        }
        addFuel();
        hasEnergyTextUppdate();
    }

    //methods
    void turnOnAndOf()
    {
        isOn = !isOn;
    }

    void hasEnergyTextUppdate()
    {
        if(useEnergy) fuelAmountText.text = "Has Energy: " + el.StructerHasElectry;
    }

    void addFuel()
    {
        if (useFuel)
        {
            if (basePlate.Slots[2].isTaken)
            {
                MaterialItem it =  (MaterialItem)basePlate.Slots[2].item;
                if (it.isFuel)
                {
                    for(int i = 0; i < basePlate.Slots[2].amount;i++)
                    {
                        fuleAmount += 5;
                    }
                    basePlate.removeItem(2, basePlate.Slots[2].amount);
                    fuelAmountText.text = "Fuel amount: " + fuleAmount;
                }
            }
        }
    }
    void combinedItem()
    {
        if(startedSmelting && el.StructerHasElectry)
        {
            if (basePlate.Slots[0].isTaken && basePlate.Slots[1].isTaken)
            {
                for (int i = 0; i < CombinedRecepice.Count; i++)
                {
                    if ((CombinedRecepice[i].neededItems[0] == basePlate.Slots[0].item && CombinedRecepice[i].neededItems[1] == basePlate.Slots[1].item) || (CombinedRecepice[i].neededItems[0] == basePlate.Slots[1].item && CombinedRecepice[i].neededItems[1] == basePlate.Slots[0].item))
                    {
                        StartCoroutine(fuseItemTimer(CombinedRecepice[i].outPutItem));
                        startedSmelting = false;
                    }
                }
            }
        }
    }
    void smeltItem()
    {
        if ((startedSmelting && fuleAmount > 0 && useFuel) || (startedSmelting && el.StructerHasElectry && useEnergy))
        {
            if (basePlate.Slots[0].isTaken)
            {
                for (int i = 0; i < recipes.Count; i++)
                {
                    if (recipes[i].neededItems[0] == basePlate.Slots[0].item)
                    {
                        StartCoroutine(smelterTimmer(recipes[i].outPutItem));
                        startedSmelting = false;
                    }
                }
            }
        }
    }
    IEnumerator fuseItemTimer(Item outputitem)
    {
        float Slidertimer;
        float timer = 0f;
        basePlate.removeItem(0, 1);
        basePlate.removeItem(1, 1);
        while (timer <= waitTime)
        {
            yield return new WaitForSeconds(1);
            Slidertimer = timer / waitTime;
            timer++;
            timerSlider.value = Slidertimer;
        }
        basePlate.PlayerInventory.addItemToInvetory(outputitem, true, true, basePlate.Slots.Count - 1, basePlate.Slots);
        startedSmelting = true;
    }

    IEnumerator smelterTimmer(Item outputitem)
    {
        float Slidertimer;
        float timer = 0f;
        basePlate.removeItem(0, 1);
        while (timer <= waitTime)
        {
            yield return new WaitForSeconds(1);
            Slidertimer = timer / waitTime;
            timer++;
            timerSlider.value = Slidertimer;
        }
        basePlate.PlayerInventory.addItemToInvetory(outputitem, true, true, basePlate.Slots.Count-1, basePlate.Slots);
        if (useFuel)
        {
            fuleAmount--;
            fuelAmountText.text = "Fuel amount: " + fuleAmount;
        }
        startedSmelting = true;
    }
    void uppdateModeText()
    {
        if(modeNum == 0)
        {
            modeText.text = "Mode: Smelter";
        }
        else
        {
            modeText.text = "Mode: Combinder";
        }
    }
    void changeMode()
    {
        if (modeNum == 1) modeNum = 0;
        else modeNum++;
        uppdateModeText();
    }
}
