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

    void Update()
    {
        if (isDead)
            return; // сли враг мертв, ничего не делаем


        FindClosestPlayer();

        // если есть цель, двигаемся к ней
        if (targetPlayer != null && Vector2.Distance(transform.position, targetPlayer.position) > stoppingDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, targetPlayer.position, speed * Time.deltaTime);
        }

        // Проверка 
        if (targetPlayer != null && Vector2.Distance(transform.position, targetPlayer.position) <= attackRange)
        {
            AttackPlayer();
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
            // урон игроку
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
        EnemySpawner.Instance.EnemyDied(); // уведомляем спавнер, что враг мертв
    }
}
