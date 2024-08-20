using UnityEngine;

public class MenuMusicManager : MonoBehaviour
{
    private AudioSource audioSource;

    private void Start()
    {
        // Получаем компонент AudioSource
        audioSource = GetComponent<AudioSource>();
    }

    public void StopMusic()
    {
        // Останавливаем воспроизведение музыки
        if (audioSource != null)
        {
            audioSource.Stop();
        }
    }
}
