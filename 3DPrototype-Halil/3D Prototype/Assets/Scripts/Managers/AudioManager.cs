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
        // Singleton yapısı
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Sahne değiştiğinde objeyi yok etme
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

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded; 
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded; // Abonelikten çık
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        PlayMusicForScene(scene.name); // Sahne yüklendiğinde müziği çal
    }

    public void PlayMusicForScene(string sceneName)
    {
        // Listede bu sahneye ait müzik bul
        SceneMusic sceneMusic = sceneMusicList.Find(music => music.sceneName == sceneName);
        if (sceneMusic != null && sceneMusic.musicClip != null)
        {
            if (audioSource.clip != sceneMusic.musicClip) // Zaten çalınan müzikle aynı değilse
            {
                audioSource.clip = sceneMusic.musicClip;
                audioSource.Play();
            }
        }
        else
        {
            Debug.LogWarning($"Bu sahne ({sceneName}) için atanmış bir müzik bulunamadı!");
            audioSource.Stop(); // Sahneye özel müzik yoksa müziği durdur
        }
    }

    /// <summary>
    /// Mevcut çalan müziği durdur
    /// </summary>
    public void StopMusic()
    {
        audioSource.Stop();
    }

    /// <summary>
    /// Mevcut çalan müziği geçici olarak duraklat
    /// </summary>
    public void PauseMusic()
    {
        audioSource.Pause();
    }

    /// <summary>
    /// Duraklatılan müziği devam ettir
    /// </summary>
    public void ResumeMusic()
    {
        audioSource.UnPause();
    }

    /// <summary>
    /// Müziğin ses seviyesini ayarla
    /// </summary>
    /// <param name="volume">0 ile 1 arasında bir ses seviyesi</param>
    public void SetVolume(float volume)
    {
        audioSource.volume = Mathf.Clamp01(volume); // Ses 0-1 arasında kalacak şekilde sınırla
    }
}
