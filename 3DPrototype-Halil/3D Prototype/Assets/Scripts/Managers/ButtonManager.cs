using NUnit.Framework;
using System.Collections;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    public GameObject canvasControls;
    public GameObject settingsPanel;
    public GameObject pauseMenu;

    public UIManager uiManager;

    //PauseMenuButtonControls

    private void Awake()
    {
        uiManager=FindObjectOfType<UIManager>();
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
      GameManager.Instance.LoadScene("MainMenu");
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
        pauseMenu.SetActive(true);
    }

    
}
