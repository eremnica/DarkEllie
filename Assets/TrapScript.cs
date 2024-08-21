using UnityEngine;

public class LeverTrap2D : MonoBehaviour
{
    public GameObject trapTile; // Ссылка на тайл-ловушку, который должен исчезать и появляться
    public float detectionRange = 2f; // Радиус, в котором рычаг может быть активирован
    private bool isTriggered = false; // Флаг для отслеживания состояния рычага

    private void Update()
    {
        // Проверяем, находится ли игрок рядом с рычагом
        if (Vector2.Distance(transform.position, PlayerPosition()) <= detectionRange && Input.GetKeyDown(KeyCode.E))
        {
            // Переключаем состояние ловушки
            ToggleTrap();
        }
    }

    // Получение позиции игрока
    private Vector2 PlayerPosition()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            return player.transform.position;
        }
        return Vector2.zero;
    }

    // Метод для переключения состояния ловушки
    private void ToggleTrap()
    {
        if (trapTile != null)
        {
            // Если ловушка активна (отображается), то отключаем её, иначе включаем
            trapTile.SetActive(!trapTile.activeSelf);
        }
    }
}
