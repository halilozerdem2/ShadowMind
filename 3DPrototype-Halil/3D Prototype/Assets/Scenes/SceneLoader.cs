using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public Scene currentScene;
    private void Update()
    {
        currentScene = SceneManager.GetActiveScene();
    }

    
}
