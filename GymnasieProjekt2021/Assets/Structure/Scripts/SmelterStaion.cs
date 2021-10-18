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
    //scripts
    private StructureBasePlateUI basePlate;
    //vars
    public bool isOn = true;
    public float waitTime;
    private bool startedSmelting = true;


    private void Start()
    {
        basePlate = GetComponent<StructureBasePlateUI>();
        isSmelterOnBtn.GetComponent<Button>().onClick.AddListener(turnOnAndOf);
    }
    private void Update()
    {
        if (isOn)
        {
            smeltItem();
        }
    }

    //methods
    void turnOnAndOf()
    {
        isOn = !isOn;
    }
    void smeltItem()
    {
        if (basePlate.Slots[0].isTaken)
        {
            for (int i = 0; i < recipes.Count; i++)
            {
                if (recipes[i].neededItems[0] == basePlate.Slots[0].item)
                {
                    if (startedSmelting)
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
        while (timer <= waitTime)
        {
            yield return new WaitForSeconds(1);
            Slidertimer = timer / waitTime;
            timer++;
            timerSlider.value = Slidertimer;
        }
        basePlate.removeItem(0, 1);
        basePlate.PlayerInventory.addItemToInvetory(outputitem, true, true, 1, basePlate.Slots);
        startedSmelting = true;
    }
}
