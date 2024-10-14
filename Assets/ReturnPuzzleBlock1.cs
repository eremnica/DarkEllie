using UnityEngine;

public class ResettableBlock : MonoBehaviour
{
    [SerializeField] private float respawnYThreshold = -10f; // Уровень, ниже которого блок возвращается
    [SerializeField] private Vector3 respawnPosition;  // Позиция для возвращения блока

    private Rigidbody2D rb;

    private void Awake()
    {
        // Запоминаем стартовую позицию блока
        respawnPosition = transform.position;

        // Получаем компонент Rigidbody2D
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // Если блок опустился ниже порогового значения по Y, возвращаем его на исходную позицию
        if (transform.position.y < respawnYThreshold)
        {
            Respawn();
        }
    }

    private void Respawn()
    {
        // Сбрасываем положение блока на исходную позицию
        transform.position = respawnPosition;

        // Сбрасываем вращение блока (если есть)
        transform.rotation = Quaternion.identity;

        // Останавливаем движение блока
        rb.linearVelocity = Vector2.zero;
        rb.angularVelocity = 0f;
    }
}
