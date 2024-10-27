using UnityEngine;

public class Boss : MonoBehaviour
{
    public GameObject fallingItem; // Префаб предмета, который будет падать
    public GameObject Board; // Деревяшка для взаимодействия
    public Transform dropPosition; // Позиция, откуда будет падать предмет
    public Enemy targetEnemy; // Ссылка на врага
    public float interactionRange = 2f; // Расстояние взаимодействия

    private Transform player; // Ссылка на игрока

    void Start()
    {
        player = GameObject.FindWithTag("Player")?.transform;

        if (targetEnemy == null)
        {
            GameObject foundEnemy = GameObject.FindWithTag("Boss");
            if (foundEnemy != null)
            {
                targetEnemy = foundEnemy.GetComponent<Enemy>();
                if (targetEnemy == null)
                {
                    Debug.LogError("Не удалось найти компонент Enemy на объекте с тегом 'Boss'.");
                }
            }
            else
            {
                Debug.LogError("Не удалось найти объект с тегом 'Boss'.");
            }
        }
    }

    void Update()
    {
        // Проверяем нажатие клавиши для обрезания веревки и расстояние до деревяшки
        if (Input.GetKeyDown(KeyCode.E) && Vector2.Distance(player.position, Board.transform.position) <= interactionRange)
        {
            CutRope();
        }
    }

    public void CutRope()
    {
        if (targetEnemy == null)
        {
            Debug.LogWarning("TargetEnemy не назначен!");
            return;
        }

        // Удаляем платформу и деревяшку
        Destroy(dropPosition.gameObject);
        Destroy(Board);

        // Отвязываем падающий предмет
        fallingItem.transform.parent = null;

        // Включаем гравитацию, если она была отключена
        Rigidbody2D rb = fallingItem.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.isKinematic = false;
        }

        // Добавляем компонент для обработки столкновения
        FallingItem fallingItemScript = fallingItem.GetComponent<FallingItem>();
        if (fallingItemScript == null)
        {
            fallingItemScript = fallingItem.AddComponent<FallingItem>();
        }
        fallingItemScript.targetEnemy = targetEnemy;
    }

    private class FallingItem : MonoBehaviour
    {
        public Enemy targetEnemy;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Boss") && targetEnemy != null)
            {
                Vector2 attackDirection = Vector2.down;
                Debug.LogWarning("Предмет упал на врага");

                targetEnemy.TakeDamage(4, attackDirection);

                Destroy(gameObject);
            }
        }
    }
}
