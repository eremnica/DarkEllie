using UnityEngine;
using System.Collections;

public class DisappearingBlock : MonoBehaviour
{
    public float disappearTime = 0.1f; // Время до исчезновения
    public float reappearTime = 0.1f; // Время до появления

    private bool isVisible = true; // Текущее состояние блока (видим или нет)

    public void Disappear()
    {
        if (isVisible)
        {
            StartCoroutine(DisappearCoroutine());
        }
    }

    public void Reappear()
    {
        if (!isVisible)
        {
            StartCoroutine(ReappearCoroutine());
        }
    }

    private IEnumerator DisappearCoroutine()
    {
        isVisible = false; // Устанавливаем состояние в "невидим"
        yield return new WaitForSeconds(disappearTime);

        gameObject.SetActive(false); // Отключаем объект
        Debug.Log("Block disappeared!");
    }

    private IEnumerator ReappearCoroutine()
    {
        yield return new WaitForSeconds(reappearTime);

        gameObject.SetActive(true); // Включаем объект
        isVisible = true; // Устанавливаем состояние в "видим"
        Debug.Log("Block reappeared!");
    }
}
