
using UnityEngine;

public class CharacterAttack : MonoBehaviour
{
    public int health = 5;
    public Transform attackPoint;
    public float attackRange = 1f;
    public int damage = 1;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            Attack();
        }
    }

    void Attack()
    {
        // поиск всех объектов в радиусе атаки
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange);

        foreach (Collider2D enemy in hitEnemies)
        {
            if (enemy.CompareTag("Enemy"))
            {
                enemy.GetComponent<Enemy>().TakeDamage(damage);
            }
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            // потом сделаю логику смерти игрока - перезапуск уровня и тд
        }
    }

    // визуализация радиуса атаки в редакторе
    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}

