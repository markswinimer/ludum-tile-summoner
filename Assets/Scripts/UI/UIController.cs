using UnityEngine;

public class UIController : MonoBehaviour
{
    public GameObject instructionsPanel;  // Assign this in the inspector

    void Start()
    {
        instructionsPanel.SetActive(false);  // Hide on start
    }

    void Update()
    {
        Debug.Log("I key pressed");
        // Check if 'I' key is pressed
        if (Input.GetKeyDown(KeyCode.I))
        {
            ToggleInstructions();
        }
    }

    public void ToggleInstructions()
    {
        instructionsPanel.SetActive(!instructionsPanel.activeSelf);  // Toggle visibility
    }
}
