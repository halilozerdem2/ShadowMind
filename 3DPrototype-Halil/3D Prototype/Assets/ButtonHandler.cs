using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonHandler : MonoBehaviour
{
    public GameObject tasksPanel;
    public GameObject inventoryPanel;

    public void Continue()
    {
        GameManager.Instance.SetGameState(GameManager.GameState.Playing);
    }
    public void OpenAndCloseTasksPanel()
    {
        tasksPanel?.SetActive(!tasksPanel.gameObject.activeSelf);
    }

    public void OpenAndCloseInventoryPanel()
    {
        inventoryPanel?.SetActive(!inventoryPanel.gameObject.activeSelf);
    }

   public void MainMenu()
    {
        StartCoroutine(LoadSceneCoroutine("MainMenu"));
    }
    public void Settings()
    {

    }
    public void Quit()
    {
        Application.Quit();
    }

    IEnumerator LoadSceneCoroutine(string sceneName)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        while (!asyncLoad.isDone)
        {
            // Yükleniyor...
            yield return null;
        }
    }
}
