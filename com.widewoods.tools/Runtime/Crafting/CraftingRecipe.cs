using System;
using UnityEngine;
using ItemRequirements = System.Collections.Generic.Dictionary<ItemData, int>;

[CreateAssetMenu(fileName = "NewCraftingRecipe", menuName = "Crafting/Recipe")]
public class CraftingRecipe : ScriptableObject
{

    [SerializeField] private ItemStack[] requirements;
    [SerializeField] private ItemStack result;

    public ItemRequirements Requirements => Crafter.BuildItemDictFromArray(requirements);
    public ItemStack Result => result;
}

