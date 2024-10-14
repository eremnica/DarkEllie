using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI;  // Панель с кнопками Restart и Quit
    private bool isPaused = false;

    void Update()
    {
        // Открытие/закрытие меню по нажатию кнопки "Escape"
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);  // Скрываем меню
        Time.timeScale = 1f;           // Возобновляем игру
        isPaused = false;
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);   // Показываем меню
        Time.timeScale = 0f;           // Останавливаем игру
        isPaused = true;
    }

    public void RestartLevel()
    {
        Time.timeScale = 1f; // Сбрасываем скорость времени, чтобы игра не была заморожена после перезапуска
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Перезапуск текущего уровня
    }

    public void QuitToMainMenu()
    {
        Time.timeScale = 1f; // Сбрасываем скорость времени перед выходом в главное меню
        SceneManager.LoadScene("Main menu"); // Загрузка сцены главного меню (укажите точное имя сцены)
    }
}
