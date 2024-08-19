using UnityEngine;
using TMPro;

public class ItemTextDisplay : MonoBehaviour
{
    public Transform player; // Ссылка на трансформ игрока
    public float detectionRange = 5f; // Радиус, на котором отображается надпись
    public TextMeshProUGUI textMeshPro; // Ссылка на компонент TextMeshPro
    private bool isPickedUp = false; // Флаг для отслеживания подбора предмета

    private void Start()
    {
        // Убедитесь, что текст по умолчанию скрыт
        if (textMeshPro != null)
        {
            textMeshPro.gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        // Проверка расстояния между игроком и объектом
        float distance = Vector3.Distance(player.position, transform.position);

        // Показать или скрыть текст в зависимости от расстояния и состояния предмета
        if (distance <= detectionRange && !isPickedUp)
        {
            ShowText();
        }
        else if (isPickedUp)
        {
            HideText();
        }
        else
        {
            HideText();
        }
    }

    private void ShowText()
    {
        if (textMeshPro != null)
        {
            textMeshPro.gameObject.SetActive(true);
            // Обновляем позицию текста над предметом
            Vector3 screenPosition = Camera.main.WorldToScreenPoint(transform.position + new Vector3(0, 2, 0));
            textMeshPro.transform.position = screenPosition;
        }
    }

    private void HideText()
    {
        if (textMeshPro != null)
        {
            textMeshPro.gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Проверка, является ли объект игроком
        if (other.CompareTag("Player"))
        {
            // Устанавливаем флаг, что предмет подобран
            isPickedUp = true;

            // Скрываем текст
            HideText();

        }
    }
}
