using UnityEngine;

public class ElevatorBlock : MonoBehaviour
{
    public float liftHeight = 5f;       // Высота подъема лифта
    public float liftSpeed = 2f;        // Скорость подъема/опускания лифта
    public ButtonTrigger buttonTrigger; // Ссылка на скрипт ButtonTrigger

    private Vector3 startPosition;      // Начальная позиция лифта (Vector3)
    private Vector3 targetPosition;     // Целевая позиция для подъема (Vector3)
    private bool isLifting = false;     // Флаг для управления движением лифта

    private void Start()
    {
        // Запоминаем начальную позицию лифта
        startPosition = transform.position;

        // Устанавливаем целевую позицию для подъема
        targetPosition = new Vector3(startPosition.x, startPosition.y + liftHeight, startPosition.z);
    }

    private void Update()
    {
        // Проверяем, активирована ли кнопка
        if (buttonTrigger.IsActivated)
        {
            // Если кнопка активирована, поднимаем лифт
            isLifting = true;
            targetPosition = new Vector3(startPosition.x, startPosition.y + liftHeight, startPosition.z);
        }
        else if (!buttonTrigger.IsActivated)
        {
            // Если кнопка деактивирована, опускаем лифт
            isLifting = true;
            targetPosition = startPosition;
        }

        // Если лифт движется, перемещаем его к целевой позиции
        if (isLifting)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, liftSpeed * Time.deltaTime);

            // Если лифт достиг целевой позиции, останавливаем движение
            if (Vector3.Distance(transform.position, targetPosition) < 0.01f)
            {
                transform.position = targetPosition; // Обеспечиваем точное попадание в целевую позицию
                isLifting = false;
            }
        }
    }
}
