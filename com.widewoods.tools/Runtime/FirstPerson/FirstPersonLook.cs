using UnityEngine;
using UnityEngine.InputSystem;

public class FirstPersonLook : MonoBehaviour
{

    [Header("References")]
    [SerializeField] private Transform cameraPivot;

    [Header("Sensitivity")]
    [SerializeField] private float sensX = 200f;
    [SerializeField] private float sensY = 200f;

    [Header("Clamp")]
    [SerializeField] private float minPitch = -80f;
    [SerializeField] private float maxPitch = 80f;

    private Vector2 mouseInput;

    private float yaw;
    private float pitch;
    private float yawVelocity;
    private float pitchVelocity;

    private void Update()
    {
        if (Cursor.lockState != CursorLockMode.Locked)
        {
            if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
            return;
        }

        float mouseX = mouseInput.x * sensX * Time.deltaTime;
        float mouseY = mouseInput.y * sensY * Time.deltaTime;

        mouseY = -mouseY;

        float targetYaw = yaw + mouseX;
        float targetPitch = pitch + mouseY;
        targetPitch = Mathf.Clamp(targetPitch, minPitch, maxPitch);

        yaw = targetYaw;
        pitch = targetPitch;

        transform.localRotation = Quaternion.Euler(0f, yaw, 0f);
        cameraPivot.localRotation = Quaternion.Euler(pitch, 0f, 0f);

        if (Keyboard.current != null && Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    private void OnApplicationFocus(bool hasFocus)
    {
        if (!hasFocus) return;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void OnMouseLook(InputValue value)
    {
        mouseInput = value.Get<Vector2>();
    }
}
