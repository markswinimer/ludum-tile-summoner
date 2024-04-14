using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuManager : MonoBehaviour
{
    public GameObject menuPanel;  // Assign the panel through the inspector
    private bool isPaused = false; // Track pause state

    void Start()
    {
        menuPanel.SetActive(false);  // Hide the menu at start
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    public void TogglePause()
    {
        isPaused = !isPaused;  // Toggle the state
        menuPanel.SetActive(isPaused);  // Set the menu panel active state based on the pause state
        Time.timeScale = isPaused ? 0 : 1;  // Pause or unpause the game

        // Optionally disable player controls when the game is paused
        if (isPaused)
        {
            // Disable player controls here
            DisablePlayerControls();
        }
        else
        {
            // Enable player controls here
            EnablePlayerControls();
        }
    }

    private void DisablePlayerControls()
    {
        // Assuming you have a script handling player movement
        Player player = FindFirstObjectByType<Player>();
        if (player != null)
        {
            player.isPlayerControllable = false;
        }
    }

    private void EnablePlayerControls()
    {
        // Assuming you have a script handling player movement
        Player player = FindFirstObjectByType<Player>();
        if (player != null)
        {
            player.isPlayerControllable = true;
        }
    }
    public void CloseMenu()
    {
        TogglePause();
    }
    public void RespawnPlayer()
    {
        Debug.Log("RESPAWN PLAYER");
        // add code for respawning player here
    }
    public void ExitGame()
    {
        Debug.Log("Game Should exit");
        Application.Quit();
    }
    public void RestartGame()
    {
        Debug.Log("Game Should restart");
        SceneManager.LoadScene("StartScreen");
    }
}