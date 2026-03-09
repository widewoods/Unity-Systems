using System.Collections.Generic;

public class CraftingChecker : ICraftingRequirementChecker
{
    public bool CanCraftFromInventory(Dictionary<ItemData, int> requirements, Dictionary<ItemData, int> inventory)
    {
        foreach (KeyValuePair<ItemData, int> requirement in requirements)
        {
            if (!inventory.ContainsKey(requirement.Key)) return false;
            if (inventory[requirement.Key] < requirement.Value) return false;
        }
        return true;
    }
}
