using UnityEngine;

public class LeverTrap2D : MonoBehaviour
{
    public GameObject trapTile;  // Ссылка на тайл-ловушку
    public float detectionRange = 2f; // Радиус активации рычага
    private GameObject closestPlayer; // Ссылка на ближайшего игрока

    private void Update()
    {
        // Находим ближайшего игрока
        closestPlayer = FindClosestPlayer();

        if (closestPlayer != null)
        {
            // Рассчитываем расстояние до ближайшего игрока
            float distance = Vector2.Distance(transform.position, closestPlayer.transform.position);

            // Проверяем, находится ли ближайший игрок в радиусе активации и нажата ли кнопка E
            if (distance <= detectionRange && Input.GetKeyDown(KeyCode.E))
            {
                // Переключаем состояние ловушки
                ToggleTrap();
            }
        }
    }

    // Метод для поиска ближайшего игрока
    private GameObject FindClosestPlayer()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        GameObject closest = null;
        float minDistance = Mathf.Infinity;

        foreach (GameObject player in players)
        {
            float distance = Vector2.Distance(transform.position, player.transform.position);

            if (distance < minDistance)
            {
                closest = player;
                minDistance = distance;
            }
        }

        return closest;
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
