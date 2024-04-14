using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainSceneAudio : MonoBehaviour
{
    void Start()
    {
        AudioManager audioManager = FindFirstObjectByType<AudioManager>();
        if (audioManager != null)
        {
            Invoke("StopMusic", 2);  // Replace 5 with however many seconds you want the music to continue before fading starts
        }
    }

    void StopMusic()
    {
        AudioManager audioManager = FindFirstObjectByType<AudioManager>();
        if (audioManager != null)
        {
            audioManager.FadeOutAndStop(2f);  // Adjust fade time as needed
        }
    }
}
