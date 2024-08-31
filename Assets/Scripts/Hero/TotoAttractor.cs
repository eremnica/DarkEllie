using UnityEngine;

public class TotoAttractor : MonoBehaviour
{
    public KeyCode attractKey = KeyCode.Z; // Клавиша для привлечения врага
    public float attractionRadius = 10f;  // Радиус, в пределах которого враги будут привлекаться

    private bool isAttracting = false; // Состояние привлечения

    void Update()
    {
        if (Input.GetKeyDown(attractKey))
        {
            ToggleAttraction();
        }
    }

    void ToggleAttraction()
    {
        isAttracting = !isAttracting;

        // Находим всех врагов на сцене
        Enemy[] enemies = FindObjectsOfType<Enemy>();

        // Привлекаем или останавливаем привлечение в зависимости от состояния
        foreach (Enemy enemy in enemies)
        {
            float distanceToEnemy = Vector2.Distance(transform.position, enemy.transform.position);

            if (isAttracting && distanceToEnemy <= attractionRadius)
            {
                // Привлекаем врагов в радиусе
                enemy.AttractTo(transform);
            }
            else if (!isAttracting || distanceToEnemy > attractionRadius)
            {
                // Останавливаем привлечение врагов, если они вне радиуса или привлечение отключено
                enemy.StopAttraction();
            }
        }
    }
}
