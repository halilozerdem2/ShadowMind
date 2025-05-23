using UnityEngine;

public class TaskOneCompleted : MonoBehaviour
{
    TaskManager taskManager;

    private void OnEnable()
    {
        taskManager = FindAnyObjectByType<TaskManager>();

    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Trash"))
        {
           taskManager.CompleteCurrentTask();
        }
    }
}
