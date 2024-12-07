using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TaskManager : MonoBehaviour
{
    public  List<Task> Tasks;
    public SceneLoader sceneloader;
    private int currentTaskIndex = 0;

    private void Awake()
    {
        Tasks = new List<Task>();
        sceneloader=FindAnyObjectByType<SceneLoader>();
    }

    public void AddTask(Task task)
    {
        Tasks.Add(task);
    }

    public Task GetCurrentTask()
    {
        return Tasks.Count > currentTaskIndex ? Tasks[currentTaskIndex] : null;
    }

    public void CompleteCurrentTask()
    {
        if (currentTaskIndex < Tasks.Count)
        {
            Tasks[currentTaskIndex].IsCompleted = true;

            currentTaskIndex++;
        }

        if (currentTaskIndex >= Tasks.Count)
        {
            currentTaskIndex=0;
            sceneloader.LoadNextScene();
        }
    }
}
