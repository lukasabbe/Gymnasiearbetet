using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftingStructure : MonoBehaviour
{
    public List<RecipeScriptObject> Recipes = new List<RecipeScriptObject>();
    public List<GameObject> RecipeBtns = new List<GameObject>();
    public GameObject ContentRecipesPannel;
    public GameObject CraftPannel;
    public Button craft_Btn;
    private int selectedRecipe;
    private void Start()
    {
        generateRecipetPannel();
        craft_Btn.onClick.AddListener(craft);
    }
    private void generateRecipetPannel()
    {
        GameObject Template = ContentRecipesPannel.transform.GetChild(1).gameObject;
        for (int i = 0; i < Recipes.Count; i++)
        {
            GameObject btn_recepie = Instantiate(Template, new Vector3(Template.transform.position.x , Template.transform.position.y + (i * -45) , Template.transform.position.z), Quaternion.identity, ContentRecipesPannel.transform);
            btn_recepie.transform.GetChild(0).GetComponent<Text>().text = Recipes[i].outPutItem.ItemName;
            btn_recepie.transform.GetChild(1).GetComponent<Image>().sprite = Recipes[i].outPutItem.Sprite;
            int temmpInt = i;
            btn_recepie.GetComponent<Button>().onClick.AddListener(delegate { changeSelectedItem(temmpInt); });
            RecipeBtns.Add(btn_recepie);
        }
        Destroy(Template);
    }
    private void changeSelectedItem(int indexOfRecipe)
    {
        selectedRecipe = indexOfRecipe;
        GameObject itemsNeeded = CraftPannel.transform.GetChild(1).gameObject;
        if(itemsNeeded.transform.childCount > 0)
        {
            for(int i = 1; i < itemsNeeded.transform.childCount; i++)
            {
                Destroy(itemsNeeded.transform.GetChild(i).gameObject);
            }
        }
        CraftPannel.transform.GetChild(0).GetComponent<Text>().text = "To craft: " + Recipes[selectedRecipe].outPutItem.ItemName;

        for(int i = 0; i < Recipes[selectedRecipe].neededItems.Count; i++)
        {
            GameObject neededItem = Instantiate(itemsNeeded.transform.GetChild(0).gameObject, new Vector3(itemsNeeded.transform.GetChild(0).transform.position.x, itemsNeeded.transform.GetChild(0).transform.position.y + (i * -15), itemsNeeded.transform.GetChild(0).transform.position.z), Quaternion.identity, itemsNeeded.transform);
            neededItem.GetComponent<Text>().text = "-" + Recipes[selectedRecipe].neededItems[i].ItemName +" - " + Recipes[selectedRecipe].amount[i];
            neededItem.SetActive(true);
        }
    }
    private void craft()
    {
        InventoryManager playerInventory = GetComponent<StructureBasePlateUI>().PlayerInventory;
        bool hasItem = false;
        for (int i = 0; i<Recipes[selectedRecipe].neededItems.Count; i++)
        {
            for(int i2 = 0; i2 < playerInventory.Slots.Count; i2++)
            {
                if (playerInventory.Slots[i2].isTaken)
                {
                    if(Recipes[selectedRecipe].neededItems[i].id == playerInventory.Slots[i2].item.id)
                    {
                        if(Recipes[selectedRecipe].amount[i] <= playerInventory.Slots[i2].amount)
                        {
                            hasItem = true;
                        }
                    }
                }
            }
            if (!hasItem)
            {
                return;
            }
        }
        Debug.Log("yes");
        playerInventory.addItemToInvetory(Recipes[selectedRecipe].outPutItem, true);
        for (int i = 0; i < Recipes[selectedRecipe].neededItems.Count; i++)
        {
            playerInventory.removeitem(Recipes[selectedRecipe].neededItems[i], Recipes[selectedRecipe].amount[i]);
        }
    }
    
}
