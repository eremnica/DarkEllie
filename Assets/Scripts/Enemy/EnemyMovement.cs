using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float moveDistance = 3f; // Расстояние, на которое враг будет двигаться
    public float moveSpeed = 2f;    // Скорость движения
    private Vector3 startPos;       // Начальная позиция врага
    private bool movingRight = true; // Флаг для определения направления движения

    void Start()
    {
        startPos = transform.position; // Сохраняем начальную позицию врага
    }

    void Update()
    {
        if (movingRight)
        {
            // Двигаем врага вправо
            transform.position += Vector3.right * moveSpeed * Time.deltaTime;

            // Если враг достиг максимального расстояния вправо, меняем направление
            if (transform.position.x >= startPos.x + moveDistance)
            {
                movingRight = false;
            }
        }
        else
        {
            // Двигаем врага влево
            transform.position += Vector3.left * moveSpeed * Time.deltaTime;

            // Если враг достиг максимального расстояния влево, меняем направление
            if (transform.position.x <= startPos.x - moveDistance)
            {
                movingRight = true;
            }
        }
    }
}
