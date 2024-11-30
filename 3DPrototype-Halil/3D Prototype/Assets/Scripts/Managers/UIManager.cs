using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject pauseMenu;

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
}
