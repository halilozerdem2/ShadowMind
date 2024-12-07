using JetBrains.Annotations;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Game Components")]
    public TaskManager taskManager; 
    public SceneLoader sceneLoader; 
    public UIManager uiManager; 

    public GameObject InfoPanel;
    
    private void Awake()
    {
        // Singleton kontrolü
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);


        // Referansları al
        taskManager = GetComponentInChildren<TaskManager>();
        uiManager = GetComponentInChildren<UIManager>();
        sceneLoader = GetComponentInChildren<SceneLoader>();

        // SceneLoader'ı başlat
        sceneLoader.Initialize();

    }

    private void Start()
    {
        // UIManager kontrolü
        if (uiManager == null)
        {
            Debug.LogError("UIManager not found in the scene!");
        }

        // InputManager olaylarını bağla
        InputManager.Instance.OnPausePressed += HandlePauseToggle;
        InputManager.Instance.OnTaskPressed += uiManager.ToggleTaskPanel;
        InputManager.Instance.OnInventoryPressed += uiManager.ToggleInventoryPanel;

    }
    
    public void PlayGame()
    {
        if(InfoPanel!=null)
            ActivateInfoPanel();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        InputManager.Instance.EnablePlayerInput();
        InputManager.Instance.EnableUIInputs();
        Time.timeScale = 1f;
    }


    public void PauseGame()
    {
        if(InfoPanel!=null)
            DeActivateInfoPanel();
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        InputManager.Instance.DisablePlayerInput();
        InputManager.Instance.DisableUIInputs();
        Time.timeScale = 0f;
    }

    private void HandlePauseToggle()
    {
        PauseGame();
        uiManager.ShowPauseMenu();
    }

    public void DefineMorningTasks()
    {
        taskManager.Tasks.Clear();
        taskManager.AddTask(new Task("Çöp At", "Alt kattaki çöpü bul ve konteynıra at"));
        taskManager.AddTask(new Task("İlaç kullan", "Üst kattaki yatak odalarından birinde ilaçların var. Bul ve kullan"));
    }

    public void DefineNightTasks()
    {
        taskManager.Tasks.Clear();
        taskManager.AddTask(new Task("Komşun evde mi?", "Evinin karşısındaki binaya git ve kapıyı çal"));
    }
    
    public void DeActivateInfoPanel()
    {
        InfoPanel.SetActive(false);
    }
    public void ActivateInfoPanel()
    {
        InfoPanel.SetActive(true);
    }

}
