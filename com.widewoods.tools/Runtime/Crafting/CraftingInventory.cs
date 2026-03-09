using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CraftingInventory
{
    public Dictionary<ItemData, int> inventoryDictionary;
    public List<ItemStack> itemStackList;

    public void Initialize()
    {
        SyncInventoryDictToStack();
    }

    public void SyncInventoryDictToStack()
    {
        inventoryDictionary = Crafter.BuildItemDictFromArray(itemStackList.ToArray());
    }
}
