using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneLoader : MonoBehaviour
{
    public Scene CurrentScene { get; private set; }

    /// <summary>
    /// SceneLoader'ın başlatılması için gerekli işlemleri yapar.
    /// </summary>
    public void Initialize()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        CurrentScene = SceneManager.GetActiveScene();
    }

    /// <summary>
    /// Sahne yüklendiğinde çağrılan metot.
    /// </summary>
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        CurrentScene = scene;

        switch (scene.name)
        {
            case "Morning":
                HandleMorningScene();
                break;

            case "Night":
                HandleNightScene();
                break;

            case "MainMenu":
                HandleMainMenuScene();
                break;

            case "GameOver":
                HandleGameOverScene();
                break;

            default:
                Debug.LogWarning($"Tanımlanmamış bir sahne yüklendi: {scene.name}");
                break;
        }
    }

    /// <summary>
    /// Belirtilen sahneyi asenkron olarak yükler.
    /// </summary>
    /// <param name="sceneName">Yüklenecek sahnenin adı.</param>
    public void LoadNextScene()
    {
        int currentIndex = SceneManager.GetActiveScene().buildIndex;
        if (currentIndex + 1 < SceneManager.sceneCountInBuildSettings)
        {
            LoadSceneByIndex(currentIndex + 1);
        }
        else
        {
            Debug.LogWarning("Son sahneye ulaşıldı. Daha fazla sahne yok.");
        }
    }

    public void LoadSceneByIndex(int index)
    {
        GameManager.Instance.StartCoroutine(LoadSceneCoroutine(index));
    }

    /// <summary>
    /// Asenkron sahne yükleme işlemini gerçekleştirir.
    /// </summary>
     private IEnumerator LoadSceneCoroutine(int sceneIndex)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneIndex);
        asyncLoad.allowSceneActivation = false;

        while (!asyncLoad.isDone)
        {
            if (asyncLoad.progress >= 0.9f)
            {
                asyncLoad.allowSceneActivation = true;
            }
            yield return null;
        }
    }

    private void HandleMorningScene()
    {
        PlayerSpawner.Instance.ResetPlayer();
        PlayerSpawner.Instance.SpawnCharacter();
        GameManager.Instance.PlayGame();
        GameManager.Instance.DefineMorningTasks();
    }

    private void HandleNightScene()
    {
        PlayerSpawner.Instance.ResetPlayer();
        PlayerSpawner.Instance.SpawnCharacter();
        GameManager.Instance.PlayGame();
        GameManager.Instance.DefineNightTasks();
    }

    private void HandleMainMenuScene()
    {
        PlayerSpawner.Instance.ResetPlayer();
        GameManager.Instance.PauseGame();
        GameManager.Instance.taskManager.Tasks.Clear();
        Debug.Log("MainMenu scene loaded.");
    }

    private void HandleGameOverScene()
    {
        PlayerSpawner.Instance.ResetPlayer();
        GameManager.Instance.PauseGame();
        GameManager.Instance.taskManager.Tasks.Clear();
        Debug.Log("GameOver scene loaded.");
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
