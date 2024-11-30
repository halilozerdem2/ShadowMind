using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance { get; private set; }

    private PlayerControls playerControls;
    private UIControls uiControls;

    //public delegate void InputActionHandler();
    public event System.Action OnPausePressed;
    public event System.Action OnTaskPressed;
    public event System.Action OnInventoryPressed;

    public Vector2 MoveInput { get; private set; }
    public Vector2 LookInput { get; private set; }
    public bool JumpInput { get; private set; }
    public bool RunInput { get; private set; }

    public bool isEscapedPressed, isIButtonPressed, isTButtonPressed;
    public bool IsInputEnabled { get; private set; } = true;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        //DontDestroyOnLoad(gameObject);

        //Player Actions
        playerControls = new PlayerControls();

        playerControls.Player.Move.performed += ctx => MoveInput = ctx.ReadValue<Vector2>();
        playerControls.Player.Move.canceled += ctx => MoveInput = Vector2.zero;

        playerControls.Player.Look.performed += ctx => LookInput = ctx.ReadValue<Vector2>();
        playerControls.Player.Look.canceled += ctx => LookInput = Vector2.zero;

        playerControls.Player.Jump.performed += ctx => JumpInput = true;
        playerControls.Player.Jump.canceled += ctx => JumpInput = false;

        playerControls.Player.Run.performed += ctx => RunInput = true;
        playerControls.Player.Run.canceled += ctx => RunInput = false;

        //UI Controls
        uiControls = new UIControls();

        uiControls.UI.PauseGame.performed += ctx => OnPausePressed?.Invoke();
        uiControls.UI.OpenTasksPanel.performed += ctx => OnTaskPressed?.Invoke();
        uiControls.UI.OpenInventoryPanel.performed += ctx => OnInventoryPressed?.Invoke();
        //uiControls.UI.PauseGame.performed += ctx => isEscapedPressed = true;
        //uiControls.UI.PauseGame.canceled += ctx => isEscapedPressed = false;
        
        //uiControls.UI.OpenTasksPanel.performed += ctx => isTButtonPressed = true;
        //uiControls.UI.OpenTasksPanel.canceled += ctx => isTButtonPressed = false;

        //uiControls.UI.OpenInventoryPanel.performed += ctx => isIButtonPressed = true;
        //uiControls.UI.OpenInventoryPanel.canceled += ctx => isIButtonPressed = false;

    }

    private void OnEnable()
    {
        playerControls.Enable();
        uiControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
        uiControls.Disable();
    }

    public void EnablePlayerInput()
    {
        IsInputEnabled = true;
        playerControls.Enable();
    }

    public void DisablePlayerInput()
    {
        IsInputEnabled = false;
        MoveInput = Vector2.zero;
        LookInput = Vector2.zero;
        JumpInput = false;
        RunInput = false;
        playerControls.Disable();
    }

}
