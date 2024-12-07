using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.Scripting;

public class MenuButtons : MonoBehaviour
{
    SceneLoader sceneLoader;
    
    private void Awake()
    {
      sceneLoader=FindAnyObjectByType<SceneLoader>();
    }

    public void play()
    {
        sceneLoader.LoadSceneByIndex(1);
    }   
} 