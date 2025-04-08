using UnityEngine;

public class ButtonHandler : MonoBehaviour
{
    public GameObject[] objects; // Assign your objects (CPU, motherboard, etc.) in the Inspector
    private int currentIndex = 0;

    void Start()
    {
        // Deactivate all objects except the first one
        for (int i = 0; i < objects.Length; i++)
        {
            objects[i].SetActive(i == currentIndex);
        }
    }

    public void NextObject()
    {
        objects[currentIndex].SetActive(false);
        currentIndex = (currentIndex + 1) % objects.Length; // Wrap around
        objects[currentIndex].SetActive(true);
    }

    public void PreviousObject()
    {
        objects[currentIndex].SetActive(false);
        currentIndex = (currentIndex - 1 + objects.Length) % objects.Length; // Wrap around
        objects[currentIndex].SetActive(true);
    }
}
