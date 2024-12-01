[System.Serializable]
public class Task
{
    public string taskName;  // Görev adý
    public string description;  // Görev açýklamasý
    public bool isCompleted;  // Görev tamamlandý mý?
    public string sceneName;  // Görevin baðlý olduðu sahne

    public Task(string name, string desc, string scene)
    {
        taskName = name;
        description = desc;
        isCompleted = false;
        sceneName = scene;
    }
}
