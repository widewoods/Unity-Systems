using UnityEngine;

public class ExampleInteractable : MonoBehaviour, IInteractable
{
    [SerializeField] private string prompt;
    [SerializeField] private Rigidbody rb;

    public string GetInteractionPrompt()
    {
        return prompt;
    }

    public void Interact()
    {
        rb.AddForce(Vector3.up * 10, ForceMode.Impulse);
    }
}
