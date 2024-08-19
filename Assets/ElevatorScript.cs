using UnityEngine;

public class ElevatorBlock : MonoBehaviour
{
    public float liftHeight = 5f; // Высота, на которую поднимется блок
    public float liftSpeed = 2f;  // Скорость подъема/опускания
    private bool isLifting = false; // Флаг для начала движения
    private Vector2 startPosition; // Стартовая позиция блока
    private Vector2 targetPosition; // Целевая позиция (вверх или вниз)

    private void Start()
    {
        // Запоминаем стартовую позицию блока
        startPosition = transform.position;
        // Устанавливаем изначально целевую позицию для подъема
        targetPosition = new Vector2(startPosition.x, startPosition.y + liftHeight);
    }

    private void Update()
    {
        // Начало движения при нажатии на кнопку E
        if (Input.GetKeyDown(KeyCode.E))
        {
            isLifting = true;
            // Переключаем целевую позицию между стартовой и поднятой
            if ((Vector2)transform.position == startPosition)
            {
                targetPosition = new Vector2(startPosition.x, startPosition.y + liftHeight);
            }
            else if ((Vector2)transform.position == targetPosition)
            {
                targetPosition = startPosition;
            }
        }

        // Если движение активировано, плавно перемещаем блок
        if (isLifting)
        {
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, liftSpeed * Time.deltaTime);

            // Останавливаем движение, если блок достиг целевой позиции
            if ((Vector2)transform.position == targetPosition)
            {
                isLifting = false;
            }
        }
    }
}
