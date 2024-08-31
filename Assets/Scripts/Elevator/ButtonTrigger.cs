using UnityEngine;

public class ButtonTrigger : MonoBehaviour
{
    public float pressDepth = 0.2f; // Глубина, на которую кнопка опускается при нажатии
    public float pressSpeed = 2f;   // Скорость движения кнопки

    private Vector3 startPosition;  // Начальная позиция кнопки
    private Vector3 pressedPosition;// Позиция кнопки при нажатии
    private bool isPressed = false; // Состояние кнопки (нажата или нет)

    public bool IsActivated { get; private set; } // Флаг активации

    private void Start()
    {
        // Запоминаем начальную позицию кнопки
        startPosition = transform.position;

        // Рассчитываем позицию кнопки при нажатии
        pressedPosition = new Vector3(startPosition.x, startPosition.y - pressDepth, startPosition.z);
    }

    private void Update()
    {
        // Если кнопка нажата, опускаем её
        if (isPressed)
        {
            transform.position = Vector3.MoveTowards(transform.position, pressedPosition, pressSpeed * Time.deltaTime);
        }
        else
        {
            // Если кнопка не нажата, возвращаем её в исходное положение
            transform.position = Vector3.MoveTowards(transform.position, startPosition, pressSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Если персонаж наступает на кнопку, активируем её
        if (other.CompareTag("Player") || other.CompareTag("Square"))
        {
            isPressed = true;
            IsActivated = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // Если персонаж покидает кнопку, деактивируем её
        if (other.CompareTag("Player") || other.CompareTag("Square"))
        {
            isPressed = false;
            IsActivated = false;
        }
    }
}
