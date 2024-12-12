using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance; // Singleton Pattern

    [System.Serializable]
    public class SceneMusic
    {
        public string sceneName;         // Sahne adı (örneğin: "MainMenu", "Level1" vs.)
        public AudioClip musicClip;      // Bu sahneye özel müzik dosyası
    }

    [Header("Scene Music Settings")]
    public List<SceneMusic> sceneMusicList; // Tüm sahnelerin müzik bilgilerini içeren liste

    private AudioSource audioSource;
    
    public float currentVolume;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        audioSource = gameObject.AddComponent<AudioSource>(); 
        audioSource.loop = true;
        audioSource.volume=0.5f;
        currentVolume=audioSource.volume;
    }
    private void Update()
    {
        currentVolume=audioSource.volume;
    }

    public void PlayMusicForScene(string sceneName)
    {
        SceneMusic sceneMusic = sceneMusicList.Find(music => music.sceneName == sceneName);
        if (sceneMusic != null && sceneMusic.musicClip != null)
        {
            if (audioSource.clip != sceneMusic.musicClip)
            {
                audioSource.clip = sceneMusic.musicClip;
                audioSource.Play();
            }
        }
        else
        {
            Debug.LogWarning($"Bu sahne ({sceneName}) için atanmış bir müzik bulunamadı!");
            audioSource.Stop();
        }
    }


    public void StopMusic()
    {
        audioSource.Stop();
    }
    public void PauseMusic()
    {
        audioSource.Pause();
    }

    public void ResumeMusic()
    {
        audioSource.UnPause();
    }
    public void SetVolume(float volume)
    {
        audioSource.volume = Mathf.Clamp01(volume); 
    }
}
