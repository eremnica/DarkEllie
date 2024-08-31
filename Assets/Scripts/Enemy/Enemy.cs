using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 2f;
    public float stoppingDistance = 1f;
    public int health = 3;
    public float attackRange = 1f;
    public float attackCooldown = 2f;
    public int damage = 1;

    private Transform targetPlayer; // цель, за которой будет следовать враг
    private float attackTimer;
    private bool isDead = false; // Проверка, жив ли враг
    private bool isAttracted = false; // Проверка, привлекается ли враг к Тотошке

    private Transform attractedTarget; // Цель, к которой привлекается враг

    void Update()
    {
        if (isDead)
            return; // Если враг мертв, ничего не делаем

        if (isAttracted && attractedTarget != null)
        {
            // Если враг привлекается к цели, следуем за ней
            FollowTarget(attractedTarget);
        }
        else
        {
            // Иначе ищем ближайшего игрока
            FindClosestPlayer();
            if (targetPlayer != null && Vector2.Distance(transform.position, targetPlayer.position) > stoppingDistance)
            {
                FollowTarget(targetPlayer);
            }
        }

        // Проверка на возможность атаки
        if (targetPlayer != null && Vector2.Distance(transform.position, targetPlayer.position) <= attackRange)
        {
            AttackPlayer();
        }
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
            // Наносим урон игроку
            targetPlayer.GetComponent<CharacterAttack>().TakeDamage(damage);
            attackTimer = attackCooldown;
        }
        else
        {
            attackTimer -= Time.deltaTime;
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        isDead = true;
        gameObject.SetActive(false);
        EnemySpawner.Instance.EnemyDied(); // Уведомляем спавнер, что враг мертв
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
}
