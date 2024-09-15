using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CharacterHealth : MonoBehaviour
{
    public int maxHealth = 5;  // Максимальное здоровье
    private int currentHealth; // Текущее здоровье
    public Transform[] respawnPoints; // Точки респауна (3 точки)
    public Rigidbody2D rb;
    public float knockbackForce = 5f; // Сила отталкивания

    public GameObject healthBarPrefab; // Префаб шкалы здоровья
    private Image healthBarImage; // Изображение шкалы здоровья
    private Transform healthBarUI; // Канвас со шкалой здоровья

    public GameObject[] enemies; // Массив для врагов
    public float deathRange = 5f; // Промежуток, в котором считается, что герой умер возле врага

    private Transform closestRespawnPoint; // Ближайшая точка респауна

    public Button restartButton; // Ссылка на кнопку рестарта
    public Image background; // Ссылка на фоновое изображение

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;

        // Инициализация шкалы здоровья
        GameObject healthBarInstance = Instantiate(healthBarPrefab, transform.position, Quaternion.identity);
        healthBarUI = healthBarInstance.transform;

        // Поиск Image по имени в healthBarUI
        Transform healthBarImageTransform = healthBarUI.Find("Full");
        if (healthBarImageTransform != null)
        {
            healthBarImage = healthBarImageTransform.GetComponent<Image>();
        }
        else
        {
            Debug.LogError("HealthBarImage not found in healthBarUI");
        }

        // Привязываем шкалу здоровья к позиции героя
        healthBarUI.SetParent(GameObject.Find("Canvas").transform, false); // Canvas должен быть в сцене
    }

    void Update()
    {
        if (healthBarUI != null)
        {
            UpdateHealthBarPosition();
        }
    }

    // Метод для получения урона
    public void TakeDamage(int damage, Vector2 knockbackDirection)
    {
        if (currentHealth > 0)
        {
            currentHealth -= damage;

            // Обновляем шкалу здоровья
            UpdateHealthBar();

            // Применяем силу отталкивания к игроку
            rb.AddForce(knockbackDirection * knockbackForce, ForceMode2D.Impulse);

            // Если здоровье опустилось до 0
            if (currentHealth <= 0)
            {
                FindClosestRespawnPoint(); // Найти ближайшую точку респауна
                Die();
            }
        }
    }

    // Поиск ближайшей точки респауна в зависимости от положения врагов
    void FindClosestRespawnPoint()
    {
        float closestDistance = Mathf.Infinity;

        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < closestDistance && distanceToEnemy <= deathRange)
            {
                closestDistance = distanceToEnemy;

                // Найти ближайшую точку респауна в массиве respawnPoints
                closestRespawnPoint = respawnPoints[System.Array.IndexOf(enemies, enemy)];
            }
        }

        // Если ближайшая точка не найдена или герой не рядом с врагами, выбрать первую точку
        if (closestRespawnPoint == null)
        {
            closestRespawnPoint = respawnPoints[0];
        }
    }

    // Метод для смерти и рестарта игры
    void Die()
    {
        background.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);

        // Назначьте метод нажатия кнопки
        restartButton.onClick.AddListener(RestartGame);
    }

    // Показ экрана окончания игры с кнопкой рестарта
    void ShowGameOverScreen()
    {
        // Тут можно реализовать логику появления UI с кнопкой "Рестарт"
        // На UI назначьте кнопку, которая вызывает RestartGame()
        SceneManager.LoadScene("EndGame", LoadSceneMode.Additive);
        Debug.Log("Game Over. Press Restart to respawn.");
    }

    // Перезапуск уровня и респаун в нужной точке
    public void RestartGame()
    {
        //ceneManager.LoadScene(SceneManager.GetActiveScene().name); // Перезапуск сцены
        transform.position = closestRespawnPoint.position; // Респаун героя в выбранной точке
        currentHealth = maxHealth; // Восстановить здоровье
        gameObject.SetActive(true); // Активируем героя
        UpdateHealthBar(); // Обновляем шкалу здоровья

        background.gameObject.SetActive(false);
        restartButton.gameObject.SetActive(false);
        //Sэ
      

    }

    // Обновление шкалы здоровья
    void UpdateHealthBar()
    {
        if (healthBarImage != null)
        {
            healthBarImage.fillAmount = Mathf.Clamp01((float)currentHealth / maxHealth);
        }
    }

    // Обновление позиции шкалы здоровья
    void UpdateHealthBarPosition()
    {
        if (healthBarUI != null)
        {
            Vector3 healthBarWorldPosition = transform.position + new Vector3(0, 2.3f, 0);
            Vector3 screenPosition = Camera.main.WorldToScreenPoint(healthBarWorldPosition);
            healthBarUI.position = screenPosition;
        }
    }
}

