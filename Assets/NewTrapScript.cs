using UnityEngine;

public class LeverTrap2D : MonoBehaviour
{
    public GameObject trapTile; // Блок, который должен исчезать и появляться
    public float detectionRange = 2f; // Радиус активации рычага
    public KeyCode activationKey = KeyCode.E; // Клавиша для активации рычага

    private Transform playerTransform;

    private void Start()
    {
        // Находим игрока в начале игры
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerTransform = player.transform;
            Debug.Log("Игрок найден: " + player.name);
        }
        else
        {
            Debug.LogError("Игрок с тегом 'Player' не найден в сцене.");
        }

        // Проверяем, установлен ли блок ловушки
        if (trapTile != null)
        {
            Debug.Log("trapTile установлен: " + trapTile.name);
        }
        else
        {
            Debug.LogError("trapTile не установлен в инспекторе.");
        }
    }

    private void Update()
    {
        if (playerTransform == null)
            return;

        // Рассчитываем расстояние по осям X и Y
        float distance = Vector2.Distance(new Vector2(transform.position.x, transform.position.y),
                                          new Vector2(playerTransform.position.x, playerTransform.position.y));

        Debug.Log("Расстояние до игрока по осям X и Y: " + distance);

        // Проверяем, находится ли игрок в радиусе активации
        if (distance <= detectionRange)
        {
            Debug.Log("Игрок в радиусе активации.");

            if (Input.GetKeyDown(activationKey))
            {
                Debug.Log("Клавиша активации нажата.");
                ToggleTrap();
            }
        }
    }

    private void ToggleTrap()
    {
        if (trapTile != null)
        {
            // Переключаем состояние блока
            trapTile.SetActive(!trapTile.activeSelf);
            Debug.Log("Состояние trapTile переключено. Активен: " + trapTile.activeSelf);
        }
    }
}
