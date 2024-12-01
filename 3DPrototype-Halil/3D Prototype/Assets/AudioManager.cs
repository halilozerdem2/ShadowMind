using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class AudioManager : MonoBehaviour
{
    private AudioSource audioSource;

    // Müzik dosyalarýný her sahneye göre atýyoruz
    public AudioClip mainMenuMusic;
    public AudioClip morningMusic;
    public AudioClip nightMusic;
    public AudioClip gameOverMusic;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayMusicForScene(string sceneName)
    {
        switch (sceneName)
        {
            case "MainMenu":
                StartCoroutine(FadeInMusic(mainMenuMusic, 1f));
                break;
            case "Morning":
                StartCoroutine(FadeInMusic(morningMusic, 1f));
                break;
            case "Night":
                StartCoroutine(FadeInMusic(nightMusic, 1f));
                break;
            case "GameOver":
                StartCoroutine(FadeInMusic(gameOverMusic, 1f));
                break;
            default:
                break;
        }
    }

    private IEnumerator FadeInMusic(AudioClip newClip, float fadeDuration)
    {
        if (audioSource.isPlaying)
        {
            yield return FadeOutMusic(fadeDuration);
        }

        audioSource.clip = newClip;
        audioSource.Play();
        audioSource.volume = 0;

        for (float t = 0; t < fadeDuration; t += Time.deltaTime)
        {
            audioSource.volume = Mathf.Lerp(0, 1, t / fadeDuration);
            yield return null;
        }

        audioSource.volume = 1;
    }

    private IEnumerator FadeOutMusic(float fadeDuration)
    {
        float startVolume = audioSource.volume;

        for (float t = 0; t < fadeDuration; t += Time.deltaTime)
        {
            audioSource.volume = Mathf.Lerp(startVolume, 0, t / fadeDuration);
            yield return null;
        }
        audioSource.volume = 0;
        audioSource.Stop();
    }
}
