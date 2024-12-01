using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaskUI : MonoBehaviour
{
    public TaskManager taskManager;  // TaskManager referansý
    public Text taskListText;  // UI Text objesi

    // Bu metod, sahne adýna göre görev listesini günceller
    public void UpdateTaskList(string sceneName)
    {
        List<Task> tasksForScene = taskManager.GetTasksForScene(sceneName);  // Sahneye özel görevler
        taskListText.text = "";  // Önceki listeyi temizle

        foreach (Task task in tasksForScene)
        {
            string status = task.isCompleted ? "Tamamlandý" : "Devam Ediyor";
            taskListText.text += $"{task.taskName} - {status}\n";  // Görev adýný ve durumunu listele
        }
    }
}
