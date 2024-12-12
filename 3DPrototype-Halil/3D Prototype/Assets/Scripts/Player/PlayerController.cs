using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;

    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float runningSpeed = 7f;

    [Header("Look Settings")]
    [SerializeField] public float lookSensitivity = 2f;
    [SerializeField] private float cameraPitch = 0f;

    [Header("Tools")]
    public GameObject FlashLight;

    private CharacterController controller;
    private Transform playerCamera;


    private void Start()
    {
        InputManager.Instance.OnFButtonPressed+=ControlFlashLight;
    }
    private void OnEnable()
    {
        controller = GetComponent<CharacterController>();
        playerCamera = Camera.main.transform;
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
        velocity.y = Physics.gravity.y;
        controller.Move(velocity * Time.deltaTime);
    }

    public void ControlFlashLight()
    {
        bool isFlashLightActive=FlashLight.activeSelf;
        FlashLight.SetActive(!isFlashLightActive);
    }
}
