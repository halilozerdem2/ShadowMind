using UnityEngine;

public class GameSetup : MonoBehaviour
{
    public TaskManager taskManager;
    public TaskUI taskUI; // TaskUI referansý

    void Start()
    {
        // Görev tanýmlarý
        taskManager.AddTask(new Task("Anahtar Bul", "Ev kapýsýný açmak için anahtarý bul.", "Game"));
        taskManager.AddTask(new Task("Köprüden Geç", "Köprüdeki engeli kaldýr ve geç.", "Level1"));
        taskManager.AddTask(new Task("Kapýyý Aç", "Ev kapýsýný anahtar ile aç.", "level2"));

        // InputManager'dan OnTaskPressed olayýný dinle
        InputManager.Instance.OnTaskPressed += UpdateTaskList;
    }

    private void OnDestroy()
    {
        // Olay aboneliðini kaldýr
        if (InputManager.Instance != null)
        {
            InputManager.Instance.OnTaskPressed -= UpdateTaskList;
        }
    }

    void UpdateTaskList()
    {
        // TaskUI varsa görev listesini güncelle
        if (taskUI != null)
        {
            taskUI.UpdateTaskList("Game");
        }
        else
        {
            Debug.LogWarning("TaskUI referansý atanmadý!");
        }
    }
}
