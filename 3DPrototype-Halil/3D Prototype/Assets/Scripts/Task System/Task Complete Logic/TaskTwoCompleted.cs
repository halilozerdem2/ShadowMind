using UnityEngine;

public class TaskTwoCompleted : MonoBehaviour
{  
   public TaskManager taskManager;

   private void OnEnable()
   {
      taskManager=FindAnyObjectByType<TaskManager>();
   }
   private void OnTriggerEnter(Collider other)
{
    if (other.gameObject.tag == "Player")
    {
         taskManager.CompleteCurrentTask();
    }
}

}
