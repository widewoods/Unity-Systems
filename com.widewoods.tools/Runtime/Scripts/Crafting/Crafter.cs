using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.InputSystem;


// Presenter of crafting MVP architecture
public class Crafter
{
    private CraftingInventory inventory;

    private ICraftingRequirementChecker checker;

    public void Initialize(CraftingInventory inventory)
    {
        checker = new CraftingChecker();
        this.inventory = inventory;
    }

    public void Craft(CraftingRecipe recipe)
    {
        if (checker.CanCraftFromInventory(recipe.Requirements, inventory.inventoryDictionary))
        {
            RemoveUsedMaterials(recipe.Requirements);
            AddToInventory(recipe.Result);
            inventory.SyncInventoryDictToStack();
        }
        else
        {
            Debug.Log("Crafting failed");
        }
    }

    private void AddToInventory(ItemStack itemStack)
    {
        inventory.itemStackList.Add(itemStack);
    }

    private void RemoveUsedMaterials(Dictionary<ItemData, int> requirements)
    {
        foreach (ItemData itemData in requirements.Keys)
        {
            int remaining = requirements[itemData];
            for (int i = 0; i < inventory.itemStackList.Count; i++)
            {
                if (inventory.itemStackList[i].itemData == itemData)
                {
                    remaining = inventory.itemStackList[i].DecrementAndGetRemaining(remaining);
                }
                if (remaining == 0) break;
            }
        }

        for (int i = inventory.itemStackList.Count - 1; i >= 0; i--)
        {
            if (inventory.itemStackList[i].count <= 0)
                inventory.itemStackList.RemoveAt(i);
        }
    }

    public static Dictionary<ItemData, int> BuildItemDictFromArray(ItemStack[] itemStackList)
    {
        var itemDictionary = new Dictionary<ItemData, int>();
        foreach (ItemStack itemStack in itemStackList)
        {
            if (itemDictionary.ContainsKey(itemStack.itemData))
            {
                itemDictionary[itemStack.itemData] += itemStack.count;
            }
            else
            {
                itemDictionary.Add(itemStack.itemData, itemStack.count);
            }
        }
        return itemDictionary;
    }
}
