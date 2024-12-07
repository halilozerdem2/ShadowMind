using UnityEngine;
using TMPro;
using System.Collections;
using System.Threading.Tasks;
using Unity.VisualScripting;
using Microsoft.Unity.VisualStudio.Editor;

public class TaskUI : MonoBehaviour
{
    public TaskManager taskManager;
    public GameObject taskPrefab; 
    public Transform contentParent; 


    public void DisplayTasks()
    {
        // Önceki görevleri temizle
        foreach (Transform child in contentParent)
        {
            Destroy(child.gameObject);
        }

        // Yeni görevleri oluştur
        foreach (var task in taskManager.Tasks)
        {
            GameObject newTask = Instantiate(taskPrefab, contentParent);
            TaskPrefab taskPrefabScript = newTask.GetComponent<TaskPrefab>();
            taskPrefabScript.SetupTask(task.TaskName, task.IsCompleted, task.TaskDescription);
        }
    }
    private void OnEnable()
    {
        StartCoroutine(DelayedUpdateTaskDisplay());
    }

    private IEnumerator DelayedUpdateTaskDisplay()
    {
        yield return null; // Bir sonraki frame'i bekle
        DisplayTasks();
    }
   
    public void OnTaskCompleted()
    {
        taskManager.CompleteCurrentTask();
    }
}
