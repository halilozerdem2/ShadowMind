using UnityEngine;
[System.Serializable]
public class Task
{
    public string TaskName { get; private set; }
    public string TaskDescription { get; private set; }
    public bool IsCompleted { get; set; }

    public Task(string name, string description)
    {
        TaskName = name;
        TaskDescription = description;
        IsCompleted = false;
    }


}
