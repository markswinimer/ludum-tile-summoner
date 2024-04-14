using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    // button attached via TMP button UI
    public void StartGame()
    {
        Debug.Log("Start Game");
        SceneManager.LoadScene("MainScene");
    }
    public void ExitGame()
    {
        Debug.Log("Exit game");
        Application.Quit();
    }
}
