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
