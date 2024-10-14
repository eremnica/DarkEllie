using UnityEngine;

public class Boss : MonoBehaviour
{
    public GameObject fallingItem; // Префаб предмета, который будет падать
    public GameObject Board;
    public Transform dropPosition; // Позиция, откуда будет падать предмет
    public Enemy targetEnemy; // Ссылка на врага

        void Start()
        {
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
            // Проверяем нажатие клавиши для обрезания веревки
            if (Input.GetKeyDown(KeyCode.E))
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

            // Удаляем платформу
            Destroy(dropPosition.gameObject);
            Destroy(Board);

        // Отвязываем падающий предмет
        fallingItem.transform.parent = null; // Убираем его из родительской иерархии

        // Включаем гравитацию, если она была отключена
        Rigidbody2D rb = fallingItem.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.isKinematic = false; // Включаем физику, чтобы предмет мог упасть
        }

        // Добавляем компонент для обработки столкновения
        FallingItem fallingItemScript = fallingItem.GetComponent<FallingItem>();
        if (fallingItemScript == null)
        {
            fallingItemScript = fallingItem.AddComponent<FallingItem>();
        }
        fallingItemScript.targetEnemy = targetEnemy; // Передаем ссылку на врага
    }


    private class FallingItem : MonoBehaviour
        {
            public Enemy targetEnemy; // Ссылка на врага

            private void OnCollisionEnter2D(Collision2D collision)
            {
                if (collision.gameObject.CompareTag("Boss") && targetEnemy != null)
                {
                    // Определяем направление атаки (вниз)
                 Vector2 attackDirection = Vector2.down;
                Debug.LogWarning("Предмет упал на врага");
                // Наносим урон врагу
                targetEnemy.TakeDamage(4, attackDirection); // Передаем урон и направление
                //targetEnemy.Die(); // Уничтожаем врага
                //Destroy(targetEnemy);
                Destroy(gameObject); // Уничтожаем предмет после столкновения

                }
            }
        }
    }
