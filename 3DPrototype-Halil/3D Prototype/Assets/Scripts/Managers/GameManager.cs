using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    private UIManager uiManager;
    private SceneLoader loader;
    private bool isEscapeHandled = false;


    public enum GameState
    {
        MainMenu,
        Playing,
        Paused,
        GameOver
    }
    public GameState currentState;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        loader=GetComponent<SceneLoader>();
    }

    private void Start()
    {
        // Ensure UIManager reference is set
        uiManager = GetComponentInChildren<UIManager>();
        // If UIManager isn't found, log an error
        if (uiManager == null)
        {
            Debug.LogError("UIManager not found in the scene!");
        }

        InputManager.Instance.OnPausePressed += HandlePauseToggle;
        InputManager.Instance.OnTaskPressed += uiManager.ToggleTaskPanel;
        InputManager.Instance.OnInventoryPressed += uiManager.ToggleInventoryPanel;

        SetGameState(GameState.MainMenu); // Start the game in Mainmenu state
    }
    private void HandlePauseToggle()
    {
        if (currentState == GameState.Playing)
            SetGameState(GameState.Paused);
        else if (currentState == GameState.Paused)
            SetGameState(GameState.Playing);
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
                if (loader.currentScene.name != "MainMenu")
                {
                    SceneLoaderFunction("MainMenu");
                }
                uiManager.HidePauseMenu();
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                InputManager.Instance.DisablePlayerInput();
                Time.timeScale = 0f;
                break;

            case GameState.Playing:
                SceneLoaderFunction("Morning");
                InputManager.Instance.EnablePlayerInput();
                InputManager.Instance.EnableUIInputs();
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                Time.timeScale = 1f;
                uiManager.HidePauseMenu(); // Hide the pause menu if it's visible
                break;

            case GameState.Paused:
                uiManager.ShowPauseMenu(); // Show the pause menu
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                InputManager.Instance.DisablePlayerInput();
                InputManager.Instance.DisableUIInputs(); 
                Time.timeScale = 0f; // Freeze time in the game
                break;

            case GameState.GameOver:
                if (loader.currentScene.name != "GameOver")
                    StartCoroutine("GameOver");

                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                InputManager.Instance.DisablePlayerInput();
                InputManager.Instance.DisableUIInputs();
                Time.timeScale = 0f;
                break;
        }
    }

    private void Update()
    {
        HandleInput();
    }

    private void HandleInput()
    {
        if (InputManager.Instance.isEscapedPressed)
        {
            if (!isEscapeHandled)
            {
                isEscapeHandled = true;

                if (currentState == GameState.Playing)
                    SetGameState(GameState.Paused);
                else if (currentState == GameState.Paused)
                    SetGameState(GameState.Playing);
            }
            else
            {
                isEscapeHandled = false;
            }

        }

        if (InputManager.Instance.isTButtonPressed)
            uiManager.ToggleTaskPanel();

        if (InputManager.Instance.isIButtonPressed)
            uiManager.ToggleInventoryPanel();
    }


    public void SceneLoaderFunction(string sceneName)
    {
        // Belirtilen sahneyi yükler
        SceneManager.LoadScene(sceneName);
    }

}
