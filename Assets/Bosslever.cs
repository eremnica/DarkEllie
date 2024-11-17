using UnityEngine;

public class Lever : MonoBehaviour
{
    public DisappearingBlock[] disappearingBlocks; // Массив ссылок на блоки
    private bool isPlayerNearby = false; // Проверка, находится ли игрок рядом с рычагом
    private bool isActivated = false; // Проверка текущего состояния рычага

    private void Update()
    {
        // Проверяем, если игрок рядом и нажата клавиша E
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.E))
        {
            ToggleLever();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerNearby = true;
            Debug.Log("Player is near the lever.");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerNearby = false;
            Debug.Log("Player left the lever.");
        }
    }

    private void ToggleLever()
    {
        isActivated = !isActivated; // Переключаем состояние

        if (isActivated)
        {
            // Активируем исчезновение блоков
            foreach (var block in disappearingBlocks)
            {
                if (block != null)
                {
                    block.Disappear();
                }
            }
            Debug.Log("Lever activated!");
        }
        else
        {
            // Возвращаем блоки
            foreach (var block in disappearingBlocks)
            {
                if (block != null)
                {
                    block.Reappear();
                }
            }
            Debug.Log("Lever deactivated!");
        }
    }
}
