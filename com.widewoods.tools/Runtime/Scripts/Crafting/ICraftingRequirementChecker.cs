using System.Collections.Generic;
using UnityEngine;

public interface ICraftingRequirementChecker
{
    public bool CanCraftFromInventory(Dictionary<ItemData, int> requirements, Dictionary<ItemData, int> inventory);
}
