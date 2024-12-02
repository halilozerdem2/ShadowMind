using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TaskManager : MonoBehaviour
{
    public List<Task> Tasks;
    private int currentTaskIndex = 0;

    private void Awake()
    {
        Tasks = new List<Task>();
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
            GameManager.Instance.SetGameState(GameManager.GameState.PlayingNightScene);
        }
    }
}
