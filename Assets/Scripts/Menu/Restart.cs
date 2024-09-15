using UnityEngine;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour
{
    // Метод, который вызывается при нажатии на кнопку "Restart"
    public void RestartGame()
    {
        // Загружаем текущий уровень (респавн героя в начале уровня)
        SceneManager.LoadScene(1);
    }
}