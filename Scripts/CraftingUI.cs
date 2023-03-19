using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CraftingUI : MonoBehaviour
{
    [SerializeField] private EquipmentInventoryObject eq;
    [SerializeField] private List<RecipeSO> recipes;

    [SerializeField] private GameObject recipePrefab;
    [SerializeField] private GameObject recipeList;
    [SerializeField] private GameObject craftView;
    private GameObject craftImage;
    private GameObject craftStats;
    private GameObject craftCost;

    private static RecipeSO SelectedRecipe;

    private void OnEnable()
    {
        ClearRecipes();
        SpawnRecipes();
        SetCraftComponents();
    }

    private void SetCraftComponents()
    {
        craftImage = craftView.transform.Find("Image").gameObject;
        craftStats = craftView.transform.Find("Stats").gameObject;
        craftCost = craftView.transform.Find("Cost").gameObject;
    }

    private void ClearRecipes()
    {
        foreach (Transform tr in recipeList.transform)
        {
            Destroy(tr.gameObject);
        }
    }

    private void SpawnRecipes()
    {
        int craftingRange = SkillSystemManager.Instance.getLvl(SkillSet.CRAFTING) / 5;

        var craftingList = recipes.Where(x => (craftingRange >= (int)x.ItemToCraft.rarity - 1)).ToList();

        if (craftingList == null)
            return;

        foreach(var recipe in craftingList)
        {
            var go = Instantiate(recipePrefab);
            var recipeImage = go.transform.Find("Component/Body/Image");
            var recipeName = go.transform.Find("Component/Body/Name");
            var recipeCost = go.transform.Find("Component/Body/Cost");
            var recipeStatus = go.transform.Find("Component/Status");

            if (recipeImage == null || recipeName == null || recipeCost == null || recipeStatus == null)
                continue;

            if (recipeImage.GetComponent<Image>() != null)
                recipeImage.GetComponent<Image>().sprite = recipe.ItemToCraft.uiDesp;

            if (recipeName.GetComponent<TMP_Text>() != null)
                recipeName.GetComponent<TMP_Text>().text = recipe.ItemToCraft.Name;

            if (recipeCost.GetComponent<TMP_Text>() != null) {

                var str = "";

                foreach (var xd in recipe.ListOfIngredients)
                {
                    str += $"{xd.Count} x {xd.ItemType}\n";
                }

                recipeCost.GetComponent<TMP_Text>().text = str;
            }

            if (recipeStatus != null && go.gameObject.GetComponent<Button>() != null)
            {
                if (CraftingLogic.CanCraft(recipe, eq))
                {
                    recipeStatus.gameObject.SetActive(false);
                    go.gameObject.GetComponent<Button>().onClick.AddListener(() => this.ClickedRecipe(go, recipe));
                }
            }

            go.transform.SetParent(recipeList.transform);
        }
    }

    public void ClickedRecipe(GameObject go, RecipeSO recipe)
    {
        if (go == null) return;

        SelectedRecipe  = recipe;
        var recipeImage = go.transform.Find("Component/Body/Image").gameObject;
        var recipeCost = go.transform.Find("Component/Body/Cost").gameObject;

        if (craftImage.GetComponent<Image>() != null)
            craftImage.GetComponent<Image>().sprite = recipeImage.GetComponent<Image>().sprite;

        if(craftStats.GetComponent<TMP_Text>() != null)
            craftStats.GetComponent<TMP_Text>().text = $"STATS:\n{recipeCost.GetComponent<TMP_Text>().text}";
        if (craftCost.GetComponent<TMP_Text>() != null)
            craftCost.GetComponent<TMP_Text>().text = $"COST:\n{recipeCost.GetComponent<TMP_Text>().text}";
    }

    public void CraftRecipe()
    {
        CraftingLogic.CraftRecipe(eq, SelectedRecipe);
    }

}


/*

 public void CraftRecipe(EquipmentInventoryObject eq, RecipeSO SelectedRecipe)
    {
        var itemsToDelete = eq.Container.Items.Where(x => x.item.type == SelectedRecipe.ItemToCraft.type).ToList();

        if (itemsToDelete.Count == 0) return;
        Debug.Log($"items to del count: {itemsToDelete.Count}");
        foreach (var it in SelectedRecipe.ListOfIngredients)
        {
            var xd = itemsToDelete.Where(x => x.item.type == it.ItemType).FirstOrDefault();

            if (xd != null) 
                xd.amount--;
        }

        foreach (var it in eq.Container.Items)
        {
            if(CraftingLogic.AreItemsSame(it.item, SelectedRecipe.ItemToCraft))
            {
                it.amount++;
                break;              
            }
        }
    }
*/
