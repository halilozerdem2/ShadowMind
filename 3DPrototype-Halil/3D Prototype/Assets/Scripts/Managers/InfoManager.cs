using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class InfoManager : MonoBehaviour
{
    public TextMeshProUGUI activeTask,activeTaskDescription;

    private void Update ()
    {
        InformPlayer();
    }
    public void InformPlayer()
    {
        activeTask.text=GameManager.Instance.taskManager.GetCurrentTask().TaskName;
        activeTaskDescription.text=GameManager.Instance.taskManager.GetCurrentTask().TaskDescription;
    }
}
