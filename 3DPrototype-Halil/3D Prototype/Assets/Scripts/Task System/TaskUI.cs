using UnityEngine;
using TMPro;

public class TaskUI : MonoBehaviour
{
    public TextMeshProUGUI taskTitle;
    public TextMeshProUGUI taskDescription;

    public TaskManager taskManager;

    private void Start()
    {
        taskManager = FindAnyObjectByType<TaskManager>();
    }
    private void OnEnable()
    {
       UpdateTaskDisplay();
    }

    public void UpdateTaskDisplay()
    {
        taskManager = FindAnyObjectByType<TaskManager>();
        
        Task currentTask = taskManager.GetCurrentTask();
        if (currentTask != null)
        {
            taskTitle.text = currentTask.TaskName;
            taskDescription.text = currentTask.TaskDescription;
        }
        else
        {
            taskTitle.text = "Son";
        }
    }

    public void OnTaskCompleted()
    {
        taskManager.CompleteCurrentTask();
        UpdateTaskDisplay();
    }
}
