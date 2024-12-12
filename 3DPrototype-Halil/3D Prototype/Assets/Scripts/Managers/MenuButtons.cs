using Unity.VisualScripting;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.Scripting;
using UnityEngine.UI;

public class MenuButtons : MonoBehaviour
{
    SceneLoader sceneLoader;
    [SerializeField] Slider musicSlider;
    
    private void Awake()
    {
      sceneLoader=FindAnyObjectByType<SceneLoader>();
    }


    public void OnEnable()
    {
        Debug.Log(AudioManager.Instance.currentVolume);
        musicSlider.value=AudioManager.Instance.currentVolume;
    }

    public void SetMusicVolume()
    {
        AudioManager.Instance.SetVolume(musicSlider.value);

    }
    public void play()
    {
        sceneLoader.LoadSceneByIndex(1);
    }   
} 