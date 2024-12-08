using System.Collections.Generic;
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

    private AudioSource audioSource; // Müzikleri çalmak için kullanılacak ses kaynağı

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
        audioSource.loop = true; // Müzik sürekli çalacak
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded; // Sahne yüklendiğinde çağrılacak
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded; // Abonelikten çık
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        PlayMusicForScene(scene.name); // Sahne yüklendiğinde müziği çal
    }

    /// <summary>
    /// Sahne adına göre doğru müziği bul ve çal
    /// </summary>
    /// <param name="sceneName">Yüklenen sahnenin adı</param>
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
