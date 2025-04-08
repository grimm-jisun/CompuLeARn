using UnityEngine;

public class TextSwitcher : MonoBehaviour
{
    public GameObject[] texts; // Array to hold text objects in order
    public GameObject finishObject; // Reference to the last object (e.g., "Finish")
    public GameObject button; // Reference to the End button
    public GameObject nextObject; // Reference to the object to activate after finishing
    private int currentIndex = 0; // Tracks the currently active text

    public void SwitchText()
    {
        if (currentIndex < texts.Length)
        {
            texts[currentIndex].SetActive(false); // Disable the current text
            currentIndex++; // Move to the next text

            if (currentIndex < texts.Length)
            {
                texts[currentIndex].SetActive(true); // Enable the next text
            }
            else
            {
                finishObject.SetActive(false); // Hide the finish object
                button.SetActive(false); // Hide the button
                ActivateNextObject(); // Activate the next object
            }
        }
    }

    private void ActivateNextObject()
    {
        nextObject.SetActive(true); // Activate the next object
    }

    public void RestartTutorial()
    {
        // Reset tutorial to the start
        if (currentIndex > 0)
        {
            texts[currentIndex - 1].SetActive(false); // Hide the current active text
        }
        currentIndex = 0; // Reset index to start
        texts[0].SetActive(true); // Show the first text
        button.SetActive(true); // Show the button again
        nextObject.SetActive(false); // Deactivate the next object if active
    }
}
