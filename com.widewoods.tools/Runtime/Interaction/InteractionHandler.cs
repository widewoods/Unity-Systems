using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class InteractionHandler : MonoBehaviour
{
    [SerializeField] private IRaycastProvider raycastProvider;
    [SerializeField] private TextMeshProUGUI promptText;
    [SerializeField] private LayerMask layerMask;

    void Awake()
    {
        raycastProvider = GetComponent<IRaycastProvider>();
    }

    void Update()
    {
        if (raycastProvider.TryGetRaycastHit(out RaycastHit hit, layerMask))
        {
            IInteractable interactable = hit.transform.GetComponentInChildren<IInteractable>();
            if (interactable != null)
            {
                string prompt = interactable.GetInteractionPrompt();
                promptText.enabled = true;
                promptText.text = $"[E] - {prompt}";
                if (Keyboard.current.eKey.wasPressedThisFrame)
                {
                    interactable.Interact();
                }
            }
            else
            {
                promptText.enabled = false;
            }
        }
        else
        {
            promptText.enabled = false;
        }
    }
}
