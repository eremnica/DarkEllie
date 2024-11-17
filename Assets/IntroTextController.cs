using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class IntroTextController : MonoBehaviour
{
    public CanvasGroup textCanvasGroup;
    public float fadeDuration = 2f;
    public float displayTime = 3f;

    private static bool hasPlayedIntro = false; // Переменная для отслеживания, была ли заставка выполнена

    private void Start()
    {
        if (!hasPlayedIntro)
        {
            hasPlayedIntro = true; // Устанавливаем флаг
            StartCoroutine(ShowTextSequence());
        }
        else
        {
            // Если заставка уже была, сразу начинаем игровой процесс
            LoadGameplay();
        }
    }

    private IEnumerator ShowTextSequence()
    {
        textCanvasGroup.alpha = 0;
        yield return StartCoroutine(FadeCanvasGroup(textCanvasGroup, 0f, 1f, fadeDuration));
        yield return new WaitForSeconds(displayTime);
        yield return StartCoroutine(FadeCanvasGroup(textCanvasGroup, 1f, 0f, fadeDuration));
        LoadGameplay();
    }

    private IEnumerator FadeCanvasGroup(CanvasGroup canvasGroup, float startAlpha, float endAlpha, float duration)
    {
        float time = 0f;
        while (time < duration)
        {
            time += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(startAlpha, endAlpha, time / duration);
            yield return null;
        }
        canvasGroup.alpha = endAlpha;
    }

    private void LoadGameplay()
    {
        Debug.Log("Starting Gameplay...");
        Destroy(gameObject); // Уничтожаем Canvas
        // Включите игровую логику здесь, если всё в одной сцене
    }
}
