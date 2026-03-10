using UnityEngine;

public interface IInteractable
{
    public static int GetInteractionLayer()
    {
        return LayerMask.NameToLayer("Interactable");
    }
    public string GetInteractionPrompt();
    public void Interact();
}
