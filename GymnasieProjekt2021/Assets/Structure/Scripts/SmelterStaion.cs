using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SmelterStaion : MonoBehaviour
{
    public List<RecipeScriptObject> recipes = new List<RecipeScriptObject>();
    //GameObjects
    public GameObject isSmelterOnBtn;
    public Slider timerSlider;
    public Text fuelAmountText;
    //scripts
    private StructureBasePlateUI basePlate;
    //vars
    public bool isOn = true;
    public float waitTime;
    private bool startedSmelting = true;
    public bool useFuel;
    private float fuleAmount;

    private void Start()
    {
        basePlate = GetComponent<StructureBasePlateUI>();
        isSmelterOnBtn.GetComponent<Button>().onClick.AddListener(turnOnAndOf);
        if (!useFuel)
        {
            Destroy(fuelAmountText.gameObject);
        }
    }
    private void Update()
    {
        if (isOn)
        {
            smeltItem();
        }
        addFuel();
    }

    //methods
    void turnOnAndOf()
    {
        isOn = !isOn;
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
    void smeltItem()
    {
        if ((startedSmelting && fuleAmount > 0) || (startedSmelting && useFuel == false))
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
        basePlate.PlayerInventory.addItemToInvetory(outputitem, true, true, 1, basePlate.Slots);
        if (useFuel)
        {
            fuleAmount--;
            fuelAmountText.text = "Fuel amount: " + fuleAmount;
        }
        startedSmelting = true;
    }
}
