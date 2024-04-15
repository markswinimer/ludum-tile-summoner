using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;


public class StartMenu : MonoBehaviour
{
    public CanvasGroup uiElement;

    void Start()
    {
        // UI starts slightly faded. to match the music
        uiElement.alpha = 0.3f;

        // Start fade in
        StartCoroutine(FadeInUI());
    }
    IEnumerator FadeInUI()
    {
        float fadeDuration = 1f;
        while (uiElement.alpha < 1)
        {
            uiElement.alpha += Time.deltaTime / fadeDuration;
            yield return null;
        }
    }

    public void StartGame()
    {
        StartCoroutine(FadeAndLoadScene());
    }

    IEnumerator FadeAndLoadScene()
    {
        // Fade out the UI
        float fadeDuration = 1f;
        while (uiElement.alpha > 0)
        {
            uiElement.alpha -= Time.deltaTime / fadeDuration;
            yield return null;
        }

        // Load the next scene
        SceneManager.LoadScene("MainScene");
    }
    public void ExitGame()
    {
        //may need to alter this for different builds? aka web5
        Application.Quit();
    }
}
