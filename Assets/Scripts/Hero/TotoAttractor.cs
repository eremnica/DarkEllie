using UnityEngine;

public class TotoAttractor : MonoBehaviour
{
    public KeyCode attractKey = KeyCode.Z; // Клавиша для привлечения врага
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

        if (isAttracting)
        {
            // Привлекаем всех врагов к Тотошке
            foreach (Enemy enemy in enemies)
            {
                enemy.AttractTo(transform);
            }
        }
        else
        {
            // Останавливаем привлечение всех врагов
            foreach (Enemy enemy in enemies)
            {
                enemy.StopAttraction();
            }
        }
    }
}
