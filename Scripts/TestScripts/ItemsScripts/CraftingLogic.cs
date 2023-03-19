using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CraftingLogic
{ 
    public static bool CanCraft(RecipeSO item, EquipmentInventoryObject eq)
    {
        bool canCraft = true;
        if (item == null || eq == null) return false;

        foreach(var it in item.ListOfIngredients)
        {
            var list = eq.Container.Items.Where(x => (x.item.type == it.ItemType) && (x.amount >= it.Count)).ToList();

            if (list.Count <= 0)
            {
                canCraft = false;
                break;
            }
        }
        return canCraft;
    }

    public static bool AreItemsSame(Item it1, Item it2)
    {
        return it1.Name.Equals(it2.Name) && it1.type == it2.type && it1.rarity == it2.rarity && it1.strength == it2.strength;
    }

    public static void CraftRecipe(EquipmentInventoryObject eq, RecipeSO SelectedRecipe)
    {
        var itemsToDelete = eq.Container.Items.Where(x => x.item.type == SelectedRecipe.ItemToCraft.type).ToList();

        if (itemsToDelete.Count == 0) return;
        foreach (var it in SelectedRecipe.ListOfIngredients)
        {
            var xd = itemsToDelete.Where(x => x.item.type == it.ItemType).FirstOrDefault();

            if (xd != null)
                xd.amount--;
        }

        foreach (var it in eq.Container.Items)
        {
            if (CraftingLogic.AreItemsSame(it.item, SelectedRecipe.ItemToCraft))
            {
                it.amount++;
                break;
            }
        }
    }
}