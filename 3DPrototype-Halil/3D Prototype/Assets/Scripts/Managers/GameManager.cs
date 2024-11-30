using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    private UIManager uiManager;

    public enum GameState
    {
        MainMenu,
        Playing,
        Paused,
        GameOver
    }

    public GameState currentState;
    private bool isEscapePressed = false;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        // Ensure UIManager reference is set
        uiManager = FindObjectOfType<UIManager>();

        // If UIManager isn't found, log an error
        if (uiManager == null)
        {
            Debug.LogError("UIManager not found in the scene!");
        }

        SetGameState(GameState.Playing); // Start the game in Playing state
    }

    public void SetGameState(GameState newState)
    {
        // If state is already the same, no need to change
        if (currentState == newState)
            return;

        currentState = newState;
        HandleStateChange();
    }

    private void HandleStateChange()
    {
        switch (currentState)
        {
            case GameState.MainMenu:
                // Load the main menu scene (or activate main menu UI)
                break;

            case GameState.Playing:
                InputManager.Instance.EnablePlayerInput();
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                Time.timeScale = 1f;
                uiManager.HidePauseMenu(); // Hide the pause menu if it's visible
                break;

            case GameState.Paused:
                uiManager.ShowPauseMenu(); // Show the pause menu
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                InputManager.Instance.DisablePlayerInput(); // Disable player controls
                Time.timeScale = 0f; // Freeze time in the game
                break;

            case GameState.GameOver:
                // Handle game over logic (e.g., show game over screen)
                break;
        }
    }

    private void Update()
    {
        HandleEscapeInput();
        Debug.Log(currentState);
    }

    private void HandleEscapeInput()
    {
        if (InputManager.Instance.isEscapedPressed && !isEscapePressed) // Only process if it's the first press
        {
            isEscapePressed = true; // Set flag to prevent multiple state changes in one frame

            if (currentState == GameState.Playing)
            {
                SetGameState(GameState.Paused); // Pause the game
            }
            else if (currentState == GameState.Paused)
            {
                SetGameState(GameState.Playing); // Resume the game
            }
        }

        // Reset the escape flag when the key is released, allowing for the next press to be detected
        if (!InputManager.Instance.isEscapedPressed)
        {
            isEscapePressed = false;
        }
    }
}
