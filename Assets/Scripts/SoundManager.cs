using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AudioSource audioSource; // Müzik çalar

    private void Start()
    {
        if (audioSource != null && !audioSource.isPlaying)
        {
            audioSource.Play(); // Oyun başladığında müziği çal
        }
    }
}