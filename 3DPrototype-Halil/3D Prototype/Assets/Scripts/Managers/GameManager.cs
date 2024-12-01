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
    public TaskManager taskManager;
    public AudioManager audioManager;


    public enum GameState
    {
        MainMenu,
        PlayingMorningScene,
        PlayingNightScene,
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
        loader = GetComponent<SceneLoader>();
        taskManager=FindAnyObjectByType<TaskManager>();
        audioManager=FindAnyObjectByType<AudioManager>();
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
        if (currentState == GameState.PlayingMorningScene)
            SetGameState(GameState.Paused);
        else if (currentState == GameState.Paused)
            SetGameState(GameState.PlayingMorningScene);
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
                    StartCoroutine(LoadSceneCoroutine("MainMenu"));
                }
                audioManager.PlayMusicForScene("MainMenu");
                uiManager.HidePauseMenu();
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                InputManager.Instance.DisablePlayerInput();
                InputManager.Instance.DisableUIInputs();
                Time.timeScale = 0f;
                break;

            case GameState.PlayingMorningScene:
                if (loader.currentScene.name != "Morning")
                {
                    StartCoroutine(LoadSceneCoroutine("Morning"));
                }
                InputManager.Instance.EnablePlayerInput();
                InputManager.Instance.EnableUIInputs();
                taskManager.AddTask(new Task("Çöp At", "Alt kattaki çöpü bul ve konteynýra at"));
                taskManager.AddTask(new Task ("Anahtar teslim", "Evinin karþýsýndaki binada oturan komþuna anahtarý götür"));
                taskManager.AddTask(new Task ("Gazete Oku", "Evdeki gazeteyi bul ve salona býrak"));
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                Time.timeScale = 1f;
                uiManager.HidePauseMenu(); // Hide the pause menu if it's visible
                break;

            case GameState.PlayingNightScene:
                if (loader.currentScene.name != "Night")
                {
                    StartCoroutine(LoadSceneCoroutine("Night"));
                }
                InputManager.Instance.EnablePlayerInput();
                InputManager.Instance.EnableUIInputs();
                taskManager.AddTask(new Task("Antidepresan zamaný ", "Evdeki ilaçlarýný bul ve kullan Q ya basarak kullan"));
                taskManager.AddTask(new Task ("Çöp at ?", "Çöpü çýkarmamýþ mýydýn ??"));
                taskManager.AddTask(new Task ("Çatý!! ", "Çatý katýna git ve sonunu bekle"));
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                Time.timeScale = 1f;
                uiManager.HidePauseMenu();
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
                {
                    StartCoroutine(LoadSceneCoroutine("GameOver"));
                }

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

                if (currentState == GameState.PlayingMorningScene)
                    SetGameState(GameState.Paused);
                else if (currentState == GameState.Paused)
                    SetGameState(GameState.PlayingMorningScene);
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

    private IEnumerator LoadSceneCoroutine(string sceneName)
    {
        // Asenkron sahne yükleme
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        asyncLoad.allowSceneActivation = false; // Sahnenin hemen aktif olmasýný engelle

        // Sahne yüklenene kadar bekle
        while (!asyncLoad.isDone)
        {
            // Eðer sahne %90 yüklendiyse, hemen aktif hale getir
            if (asyncLoad.progress >= 0.9f)
            {
                asyncLoad.allowSceneActivation = true;
            }

            // Yükleme sýrasýnda baþka iþlemler de yapabilirsiniz (progress bar vs.)
            yield return null;
        }
    }

}
