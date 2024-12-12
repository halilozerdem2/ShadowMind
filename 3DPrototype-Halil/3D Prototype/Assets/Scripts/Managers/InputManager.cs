using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance { get; private set; }

    private PlayerControls playerControls;
    private UIControls uiControls;

    public event System.Action OnPausePressed;
    public event System.Action OnTaskPressed;
    public event System.Action OnInventoryPressed;
    public event System.Action OnFButtonPressed;
    public event System.Action OnEButtonPressed;
    public event System.Action OnQButtonPressed;

    public Vector2 MoveInput { get; private set; }
    public Vector2 LookInput { get; private set; }
    public bool JumpInput { get; private set; }
    public bool RunInput { get; private set; }

    public bool IsInputEnabled { get; private set; } = true;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        playerControls = new PlayerControls();

        playerControls.Player.Interact.performed += ctx => OnEButtonPressed?.Invoke();
        playerControls.Player.Equip.performed += ctx => OnQButtonPressed?.Invoke();
        playerControls.Player.OpenAndCloseFlashLight.performed += ctx => OnFButtonPressed?.Invoke();

        playerControls.Player.Move.performed += ctx => MoveInput = ctx.ReadValue<Vector2>();
        playerControls.Player.Move.canceled += ctx => MoveInput = Vector2.zero;

        playerControls.Player.Look.performed += ctx => LookInput = ctx.ReadValue<Vector2>();
        playerControls.Player.Look.canceled += ctx => LookInput = Vector2.zero;

        playerControls.Player.Run.performed += ctx => RunInput = true;
        playerControls.Player.Run.canceled += ctx => RunInput = false;

        uiControls = new UIControls();

        uiControls.UI.PauseGame.performed += ctx => OnPausePressed?.Invoke();
        uiControls.UI.OpenTasksPanel.performed += ctx => OnTaskPressed?.Invoke();
        uiControls.UI.OpenInventoryPanel.performed += ctx => OnInventoryPressed?.Invoke();

        EnablePlayerInput();
        EnableUIInputs();

    }

    public void EnableUIInputs()
    {
        if(uiControls!=null)
            uiControls.Enable();
    }
    public void DisableUIInputs() 
    {
        if(uiControls!=null)
            uiControls.Disable();
    }

    public void EnablePlayerInput()
    {
        if(playerControls!=null)
        {
            IsInputEnabled = true;
            playerControls.Enable();
        }
    }

    public void DisablePlayerInput()
    {
        IsInputEnabled = false;
        MoveInput = Vector2.zero;
        LookInput = Vector2.zero;
        JumpInput = false;
        RunInput = false;
        if(playerControls!=null)
            playerControls.Disable();
    }


   public bool returnEButtonValue()
    {
        if (Input.GetKeyDown(KeyCode.E))
            return true;
        
        return false;
    }

    public bool returnQButtonValue()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            return true;
        }
        return false;
    }

}
