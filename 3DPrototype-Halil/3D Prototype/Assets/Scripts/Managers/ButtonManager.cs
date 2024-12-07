using UnityEditor.SearchService;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    public GameObject canvasControls;
    public GameObject settingsPanel;
    public GameObject pauseMenu;

    public UIManager uiManager;
    public SceneLoader sceneLoader;

    private void Awake()
    {
        uiManager=FindAnyObjectByType<UIManager>();
        sceneLoader=FindAnyObjectByType<SceneLoader>();
    }
  
    public void Continue()
    {
        GameManager.Instance.PlayGame();
        uiManager.HidePauseMenu();
        
    }
    public void OpenControlsPanel()
    {   
        canvasControls.SetActive(true);
        uiManager.HidePauseMenu();
    }
    public void MainMenu()
    {
      sceneLoader.LoadSceneByIndex(0);
      uiManager.HidePauseMenu();   
    }
    public void Settings()
    {
        bool isActive = settingsPanel.activeSelf;
        settingsPanel.SetActive(!isActive);

    }
    public void Quit()
    {
        Application.Quit();
    }

   public void BackToPausePanel()
    {
        canvasControls.gameObject.SetActive(false);
        uiManager.ShowPauseMenu();
    }

    
}
