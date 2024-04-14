using System.Collections;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private AudioSource audioSource;

    void Start()
    {
        DontDestroyOnLoad(gameObject);  // prevents the audio from stopping between scenes
        audioSource = GetComponent<AudioSource>();
        audioSource.Play();
    }
    public void FadeOutAndStop(float fadeTime)
    {
        StartCoroutine(FadeOutCoroutine(fadeTime));
    }

    private IEnumerator FadeOutCoroutine(float fadeTime)
    {
        float startVolume = audioSource.volume;

        while (audioSource.volume > 0)
        {
            audioSource.volume -= startVolume * Time.deltaTime / fadeTime;
            yield return null;
        }

        audioSource.Stop();
        Destroy(gameObject);
    }
}