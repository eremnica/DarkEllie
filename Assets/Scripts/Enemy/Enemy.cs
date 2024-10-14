using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public float speed = 3f;
    public float stoppingDistance = 0.1f;
    public int maxHealth = 3; // Максимальное здоровье врага
    private int currentHealth; // Текущее здоровье

    public float attackRange = 3f; // Радиус атаки врага
    public float attackCooldown = 2f;
    public int damage = 1;
    public float detectionRange = 10f; // Радиус обнаружения игрока
    public float knockbackForce = 5f;  // Сила отталкивания

    private Transform targetPlayer; // Цель, за которой будет следовать враг
    private float attackTimer;
    public bool isDead = false; // Проверка, жив ли враг
    
    private bool isAttracted = false; // Проверка, привлекается ли враг к Тотошке

    private Transform attractedTarget; // Цель, к которой привлекается враг
    private Rigidbody2D rb; // Ссылка на Rigidbody2D для физики
    private EnemyMovement enemyMovement; // Ссылка на скрипт движения врагов

    public GameObject healthBarPrefab; // Префаб шкалы здоровья
    private Image healthBarImage; // Изображение шкалы здоровья
    private Transform healthBarUI; // Канвас со шкалой здоровья


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;

        // Получаем ссылку на скрипт движения врагов
        enemyMovement = GetComponent<EnemyMovement>();

        // Инициализация шкалы здоровья
        GameObject healthBarInstance = Instantiate(healthBarPrefab, transform.position, Quaternion.identity);
        healthBarUI = healthBarInstance.transform;

        Transform healthBarImageTransform = healthBarUI.Find("Full");
        if (healthBarImageTransform != null)
        {
            healthBarImage = healthBarImageTransform.GetComponent<Image>();
        }
        else
        {
            Debug.LogError("HealthBarImageName not found in healthBarUI");
        }

        healthBarUI.SetParent(GameObject.Find("Canvas").transform, false);
    }

    void Update()
    {
        if (isDead)
            return;

        FindClosestPlayer();

        if (targetPlayer != null && Vector2.Distance(transform.position, targetPlayer.position) <= detectionRange)
        {
            // Отключаем скрипт движения врага
            if (enemyMovement != null)
            {
                enemyMovement.enabled = false;
            }
            float distanceToPlayer = Vector2.Distance(transform.position, targetPlayer.position);
            FollowTarget(targetPlayer);
            // Если враг достаточно близко, чтобы атаков

            if (distanceToPlayer <= attackRange)
            {
                AttackPlayer();
            }
        }
        else
        {
            // Включаем скрипт движения врага
            if (enemyMovement != null)
            {
                enemyMovement.enabled = true;
            }

            
            // Уменьшаем таймер атаки (если больше 0)
        }

        if (attackTimer > 0f)
        {
            attackTimer -= Time.deltaTime;
        }
        UpdateHealthBarPosition();
    }

    void FollowTarget(Transform target)
    {
        if (target != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        }
    }


void FindClosestPlayer()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        float shortestDistance = Mathf.Infinity;
        Transform closestPlayer = null;

        foreach (GameObject player in players)
        {
            float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);
            if (distanceToPlayer < shortestDistance)
            {
                shortestDistance = distanceToPlayer;
                closestPlayer = player.transform;
            }
        }

        targetPlayer = closestPlayer;
    }

    void AttackPlayer()
    {
        if (attackTimer <= 0f)
        {
            if (targetPlayer != null && targetPlayer.GetComponent<CharacterHealth>() != null)
            {
                // Рассчитываем направление отталкивания (от врага к игроку)
                Vector2 knockbackDirection = (targetPlayer.position - transform.position).normalized;
                // Наносим урон игроку и передаем направление отталкивания
                targetPlayer.GetComponent<CharacterHealth>().TakeDamage(damage, knockbackDirection);
                attackTimer = attackCooldown; // Запускаем таймер атаки
            }
        }
    }

    public void TakeDamage(int damage, Vector2 attackDirection)
    {
        Debug.Log("Получен урон: " + damage);
        currentHealth -= damage;
        Debug.Log("Текущее здоровье: " + currentHealth);
        // Обновляем шкалу здоровья
        UpdateHealthBar();
        Debug.Log("Обновление шкалы здоровья ");

        // Добавляем эффект отталкивания врага
        rb.AddForce(attackDirection * knockbackForce, ForceMode2D.Impulse);
        Debug.LogError(string.Format($"ENEMY ({attackDirection}) ({knockbackForce}) ({ForceMode2D.Impulse})"));

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void UpdateHealthBar()
    {
        if (healthBarImage != null)
        {
            healthBarImage.fillAmount = Mathf.Clamp01((float)currentHealth / maxHealth);
            Debug.Log("Health bar updated: " + healthBarImage.fillAmount);
        }
        else
        {
            Debug.LogError("Health bar image is null");
        }
    }

    void UpdateHealthBarPosition()
    {
        if (healthBarUI != null)
        {
            Vector3 healthBarWorldPosition = transform.position + new Vector3(0.32f, 2.75f, 0); // Позиция над врагом
            Vector3 screenPosition = Camera.main.WorldToScreenPoint(healthBarWorldPosition);
            healthBarUI.position = screenPosition;
        }
    }

    public void Die()
    {
        isDead = true;
        gameObject.SetActive(false);
        if (healthBarUI != null)
        {
            Destroy(healthBarUI.gameObject); // Удаляем шкалу здоровья при смерти
        }
    }

    public void AttractTo(Transform target)
    {
        attractedTarget = target;
        isAttracted = true;
    }

    public void StopAttraction()
    {
        isAttracted = false;
        attractedTarget = null;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

}
