using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Recipe Ingredient", menuName = "Inventory System/Crafting Recipe/Crafting Recipe")]
public class RecipeSO : ScriptableObject
{
    [SerializeField] private Item itemToCraft;
    [SerializeField] private List<ItemCountSO> list;

    public List<ItemCountSO> ListOfIngredients => list;
    public Item ItemToCraft => itemToCraft;

}
