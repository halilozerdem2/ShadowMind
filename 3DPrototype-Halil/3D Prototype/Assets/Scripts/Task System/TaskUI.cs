using UnityEngine;
using TMPro;

public class TaskUI : MonoBehaviour
{
    public TextMeshProUGUI taskTitle;
    public TextMeshProUGUI taskDescription;

    private TaskManager taskManager;

    private void Start()
    {
        taskManager = FindObjectOfType<TaskManager>();
        UpdateTaskDisplay();
    }
    private void OnEnable()
    {
       UpdateTaskDisplay();
    }

    public void UpdateTaskDisplay()
    {
        Task currentTask = taskManager.GetCurrentTask();
        if (currentTask != null)
        {
            taskTitle.text = currentTask.TaskName;
            taskDescription.text = currentTask.TaskDescription;
        }
        else
        {
            taskTitle.text = "Tüm görevler tamamlandý!";
        }
    }

    public void OnTaskCompleted()
    {
        taskManager.CompleteCurrentTask();
        UpdateTaskDisplay();
    }
}
