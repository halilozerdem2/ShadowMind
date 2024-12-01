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

    //PauseMenuButtonControls
    public void Continue()
    {
        GameManager.Instance.SetGameState(GameManager.GameState.Playing);
    }
    public void OpenControlsPanel()
    {   
        canvasControls.SetActive(true);
        pauseMenu.SetActive(false);
    }
    public void MainMenu()
    {
        GameManager.Instance.SetGameState(GameManager.GameState.MainMenu);
        
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

   //CtonrolPanelButtonControls
   public void BackToPausePanel()
    {
        canvasControls.gameObject.SetActive(false);
        pauseMenu.SetActive(true);
    }

    
}
