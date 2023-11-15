using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float lookSpeed = 2f;
    [SerializeField] private float jumpForce = 7f;

    private CharacterController characterController;
    private Vector3 velocity;
    private bool isGrounded;
    private float pitch = 0f;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        HandleMovementInput();
        HandleLookInput();
        HandleJumpInput();
    }

    private void HandleMovementInput()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontal, 0f, vertical);
        movement = transform.TransformDirection(movement);

        characterController.Move(movement * speed * Time.deltaTime);

        // Check if the player is grounded
        isGrounded = characterController.isGrounded;

        // Apply gravity
        if (!isGrounded)
        {
            velocity.y += Physics.gravity.y * Time.deltaTime;
        }
        else
        {
            velocity.y = -0.5f; // A small negative value to ensure the player stays grounded
        }

        // Move the player with gravity
        characterController.Move(velocity * Time.deltaTime);
    }

    private void HandleLookInput()
    {
        float mouseX = Input.GetAxis("Mouse X") * lookSpeed;
        float mouseY = Input.GetAxis("Mouse Y") * lookSpeed;

        // Rotate the player around the Y-axis based on mouse input
        transform.Rotate(Vector3.up * mouseX);

        // Adjust the pitch (up and down) based on mouse input
        pitch -= mouseY;
        pitch = Mathf.Clamp(pitch, -90f, 90f);

        // Apply rotation to the camera/player
        Camera.main.transform.localRotation = Quaternion.Euler(pitch, 0f, 0f);

        // Lock rotation on the Z-axis
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, 0f);
    }

    private void HandleJumpInput()
    {
        if (isGrounded && Input.GetButton("Jump"))
        {
            velocity.y = Mathf.Sqrt(jumpForce * -2f * Physics.gravity.y);
        }
    }
}
