using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class InfoManager : MonoBehaviour
{
    public TextMeshProUGUI activeTask;
    
    private void Update()
    {
        activeTask.text=GameManager.Instance.taskManager.GetCurrentTask().TaskName;
    }

}
