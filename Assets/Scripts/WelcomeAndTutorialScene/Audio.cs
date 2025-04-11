using UnityEngine;

public class SimpleAudioPlayer : MonoBehaviour
{
    public AudioSource audioSource; // Drag and drop the Audio Source in the Inspector.
    public AudioClip audioClip; // Drag and drop the Audio Clip in the Inspector.

    void Start()
    {
        audioSource.clip = audioClip; // Assign the clip to the Audio Source.
        audioSource.Play(); // Play the audio at the start of the scene.
    }
}
