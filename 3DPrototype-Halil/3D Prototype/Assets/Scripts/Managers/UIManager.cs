using UnityEngine;

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
    //public void ShowControlPanel()
    //{
    //    if (controlsPanel != null)
    //    {
    //        controlsPanel.SetActive(true);
    //    }
    //    else
    //    {
    //        Debug.LogError("ControlPanel not assigned in the UIManager.");
    //    }
    //}
    //public void ShowTasksPanel()
    //{
    //    if (tasksPanel != null)
    //    {
    //        tasksPanel.SetActive(true);
    //    }
    //    else
    //    {
    //        Debug.LogError("TasksPanel not assigned in the UIManager.");
    //    }
    //}
    //public void HideTasksPanel()
    //{
    //    if (tasksPanel != null)
    //    {
    //        tasksPanel.SetActive(false);
    //    }
    //    else
    //    {
    //        Debug.LogError("tasksPanel not assigned in the UIManager.");
    //    }
    //}
    //public void HideControlPanel()
    //{
    //    if (controlsPanel != null)
    //    {
    //        controlsPanel.SetActive(false);
    //    }
    //    else
    //    {
    //        Debug.LogError("ControlPanel not assigned in the UIManager.");
    //    }
    //}


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
