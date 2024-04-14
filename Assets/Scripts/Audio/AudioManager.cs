using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private AudioSource audioSource;

    void Start()
    {
        // plays the audio when scene loads
        audioSource = GetComponent<AudioSource>();
        audioSource.Play();
    }
}
