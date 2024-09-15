using UnityEngine;

public class CharacterAttack : MonoBehaviour
{
    public Transform attackPoint;          // Точка атаки
    public float attackRange = 1f;         // Радиус атаки
    public int damage = 1;                 // Урон, наносимый врагу
    public int maxHitsBeforeItem = 6;      // Максимальное количество ударов до восстановления
    private int currentHits = 0;           // Текущее количество ударов
    private bool isNearItem = false;       // Проверка, находится ли игрок рядом с предметом
    private GameObject itemToDestroy;      // Ссылка на предмет, который нужно уничтожить

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log("Attack button pressed");
            Attack();
        }

        if (isNearItem && Input.GetKeyDown(KeyCode.E))
        {
            RestoreHits();
        }
    }

    void Attack()
    {
        // Атака возможна всегда
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange);

        foreach (Collider2D enemy in hitEnemies)
        {
            if (enemy.CompareTag("Enemy"))
            {
                Vector2 attackDirection = (enemy.transform.position - transform.position).normalized;
                enemy.GetComponent<Enemy>().TakeDamage(damage, attackDirection);
            }
        }

        // Увеличиваем количество ударов
        currentHits++;
        if (currentHits >= maxHitsBeforeItem)
        {
            Debug.Log("Find an item and press 'E' to restore hits.");
        }
    }

    void RestoreHits()
    {
        // Восстанавливаем количество ударов до максимума
        currentHits = 0;
        Debug.Log("Hits restored. You can now attack again.");

        // Делаем предмет невидимым и отключаем взаимодействие
        if (itemToDestroy != null)
        {
            // Отключаем видимость предмета
            SpriteRenderer renderer = itemToDestroy.GetComponent<SpriteRenderer>();
            if (renderer != null)
            {
                renderer.enabled = false;  // Скрываем объект
            }

            // Отключаем возможность взаимодействия с предметом
            Collider2D collider = itemToDestroy.GetComponent<Collider2D>();
            if (collider != null)
            {
                collider.enabled = false;  // Отключаем коллайдер
            }

            // Вы также можете отключить сам объект, если нужно
            itemToDestroy.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("item"))
        {
            isNearItem = true;
            itemToDestroy = collision.gameObject;  // Сохраняем ссылку на предмет
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("item"))
        {
            isNearItem = false;
            itemToDestroy = null;  // Убираем ссылку на предмет, если игрок ушел
        }
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
