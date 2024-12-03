using UnityEngine;
using UnityEngine.Scripting;

public class UIManager : MonoBehaviour
{
    [Header("UI Panels")]
    public GameObject pauseMenu;
    public GameObject controlsPanel;
    public GameObject tasksPanel;
    public GameObject inventoryPanel;


    public void ShowPauseMenu()
    {
        if (pauseMenu != null)
        {
            pauseMenu.SetActive(true);
        }
        else
        {
            Debug.LogError("PauseMenu not assigned in the UIManager.");
        }
    }
    public void HidePauseMenu()
    {
        if (pauseMenu != null)
        {
            pauseMenu.SetActive(false);
        }
        else
        {
            Debug.LogError("PauseMenu not assigned in the UIManager.");
        }
    }
    public void ToggleTaskPanel()
    {
        if(tasksPanel != null)
        {
            bool isActive = tasksPanel.activeSelf;
            tasksPanel.SetActive(!isActive);
        }
    }

    public void ToggleInventoryPanel()
    {
        if (inventoryPanel != null)
        {
            bool isActive = inventoryPanel.activeSelf;
            inventoryPanel.SetActive(!isActive);
        }
    }

}
