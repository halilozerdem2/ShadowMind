using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TaskPrefab : MonoBehaviour
{
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI statusText;
    public TextMeshProUGUI descriptionText;
    public Image taskImage;
    public Color activeColor, passiveColor;
    public void SetupTask(string title, bool IsCompleted, string description)
    {
        titleText.text = title;
        descriptionText.text = description;
        if(IsCompleted)
        {
            taskImage.color=passiveColor;
            statusText.text="Tamamlandı";
        }
        else
        {
            taskImage.color= activeColor;
            statusText.text="Aktif Görev";
        }
    }
}
