using UnityEngine;
using UnityEngine.InputSystem;

public class FirstPersonMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float gravity = -20f;
    [SerializeField] float jumpHeight = 1.5f;

    CharacterController controller;

    private Vector2 input;
    private float verticalVelocity;

    void Awake()
    {
        controller = GetComponent<CharacterController>();
        if (controller == null)
            controller = gameObject.AddComponent<CharacterController>();
    }

    void Update()
    {
        if (!controller.enabled) return;

        if (controller.isGrounded && verticalVelocity < 0f) verticalVelocity = -2f;

        verticalVelocity += gravity * 1.3f * Time.deltaTime;
        Vector3 velocity = transform.localRotation * new Vector3(input.x, 0, input.y) * moveSpeed;
        velocity.y = verticalVelocity;

        controller.Move(velocity * Time.deltaTime);
    }

    void OnMove(InputValue value)
    {
        input = value.Get<Vector2>();
    }

    void OnJump()
    {
        if (controller.isGrounded)
        {
            verticalVelocity = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
    }
}
