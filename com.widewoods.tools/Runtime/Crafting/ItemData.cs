using System;
using UnityEngine;

[CreateAssetMenu(fileName = "NewCraftingItem", menuName = "Crafting/Item")]
public class ItemData : ScriptableObject
{
    [SerializeField] private string displayName;
    [SerializeField] private string id;

    public string DisplayName => displayName;
    public string Id => id;
}

[Serializable]
public class ItemStack
{
    public ItemData itemData;
    public int count;

    public int DecrementAndGetRemaining(int amount)
    {
        if (count < amount)
        {
            int prevCount = count;
            count = 0;
            return amount - prevCount;
        }
        else
        {
            count -= amount;
            return 0;
        }
    }
}
