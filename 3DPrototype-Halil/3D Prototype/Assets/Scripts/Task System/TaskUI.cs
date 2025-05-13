using UnityEngine;
using TMPro;
using System.Collections;

public class TaskUI : MonoBehaviour
{
    public TaskManager taskManager;
    public GameObject taskPrefab; 
    public Transform contentParent; 

    private void OnEnable()
    {
        StartCoroutine(DelayedUpdateTaskDisplay());
    }


    public void DisplayTasks()
    {
        foreach (Transform child in contentParent)
        {
            Destroy(child.gameObject);
        }

        foreach (var task in taskManager.Tasks)
        {
            GameObject newTask = Instantiate(taskPrefab, contentParent);
            TaskPrefab taskPrefabScript = newTask.GetComponent<TaskPrefab>();
            taskPrefabScript.SetupTask(task.TaskName, task.IsCompleted, task.TaskDescription);
        }
    }

    private IEnumerator DelayedUpdateTaskDisplay()
    {
        yield return null;
        DisplayTasks();
    }

    public void OnTaskCompleted()
    {
        taskManager.CompleteCurrentTask();
    }
}
