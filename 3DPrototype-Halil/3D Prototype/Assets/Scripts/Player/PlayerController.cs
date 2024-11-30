using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float runningSpeed = 7f;

    [Header("Look Settings")]
    [SerializeField] public float lookSensitivity = 2f;
    [SerializeField] private float cameraPitch = 0f;

    private Rigidbody rb;
    private Transform playerCamera;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        playerCamera = Camera.main.transform;

        rb.freezeRotation = true;
    }

    private void Update()
    {
        Vector2 lookInput = InputManager.Instance.LookInput;
        float mouseX = lookInput.x * lookSensitivity;
        float mouseY = lookInput.y * lookSensitivity;

        transform.Rotate(Vector3.up * mouseX);

        cameraPitch -= mouseY;
        cameraPitch = Mathf.Clamp(cameraPitch, -90f, 90f);
        playerCamera.localRotation = Quaternion.Euler(cameraPitch, 0f, 0f);
    }

    private void FixedUpdate()
    {
        Vector2 moveInput = InputManager.Instance.MoveInput;
        bool isRunning = InputManager.Instance.RunInput; 

        float currentSpeed = isRunning ? runningSpeed : moveSpeed; 
        Vector3 moveDirection = new Vector3(moveInput.x, 0, moveInput.y);
        Vector3 velocity = transform.TransformDirection(moveDirection) * currentSpeed;

        rb.linearVelocity = new Vector3(velocity.x, rb.linearVelocity.y, velocity.z);

        if (InputManager.Instance.JumpInput && IsGrounded())
        {
            rb.AddForce(Vector3.up * 1f, ForceMode.Impulse);
        }
    }

    private bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, 1.1f);
    }
}
