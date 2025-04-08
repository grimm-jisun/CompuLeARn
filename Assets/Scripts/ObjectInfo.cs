using UnityEngine;
using TMPro;

public class ObjectInteraction : MonoBehaviour
{
    public string displayText; // Unique text to display for this object
    public AudioClip audioClip; // Unique audio clip for this object
    public float fontSize = 14f; // Font size for the text

    public GameObject dialogBox; // Reference to the dialog box GameObject
    public TextMeshProUGUI textComponent; // Reference to the TextMeshPro text component
    public AudioSource audioSource; // Reference to the AudioSource component

    private bool isAudioPlaying = false; // Flag to track audio status

    void Start()
    {
        // Ensure the dialog box is initially deactivated
        if (dialogBox != null)
        {
            dialogBox.SetActive(false);
        }

        if (audioSource != null)
        {
            audioSource.loop = false; // Ensure audio does not loop
        }
    }

    void Update()
    {
        // Hide the dialog box if audio has finished playing
        if (isAudioPlaying && audioSource != null && !audioSource.isPlaying)
        {
            HideDialogBox();
        }
    }

    void OnMouseDown() // Triggered on mouse click
    {
        TriggerInteraction();
    }

#if UNITY_ANDROID
    void OnTouch()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            TriggerInteraction();
        }
    }
#endif

    void TriggerInteraction()
    {
        // Display text and set font size
        if (dialogBox != null && textComponent != null)
        {
            dialogBox.SetActive(true);
            textComponent.text = displayText; // Set the text in the dialog box
            textComponent.fontSize = fontSize; // Set the font size
        }

        // Play audio
        if (audioSource != null && audioClip != null)
        {
            audioSource.clip = audioClip; // Set the audio clip
            audioSource.Play(); // Play the audio
            isAudioPlaying = true; // Set the flag to true
        }
    }

    public void HideDialogBox()
    {
        // Method to hide the dialog box
        if (dialogBox != null)
        {
            dialogBox.SetActive(false);
        }
        isAudioPlaying = false; // Reset the flag
    }
}
