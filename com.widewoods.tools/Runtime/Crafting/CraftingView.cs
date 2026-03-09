using UnityEngine;
using UnityEngine.InputSystem;

public class CraftingView : MonoBehaviour
{
    [SerializeField] private CraftingRecipe currentRecipe;
    [SerializeField] private CraftingInventory craftingInventory;
    private Crafter crafter;

    void Awake()
    {
        crafter = new Crafter();
        crafter.Initialize(craftingInventory);
        craftingInventory.Initialize();
    }

    void Update()
    {
        if (Keyboard.current.eKey.wasPressedThisFrame)
        {
            crafter.Craft(currentRecipe);
        }
    }
}
