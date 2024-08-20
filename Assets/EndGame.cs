using UnityEngine;

public class QuitGame : MonoBehaviour
{
    // Этот метод будет вызван при нажатии на кнопку
    public void ExitGame()
    {
        // Выходит из игры
        Application.Quit();

        // Сообщение для отладки (работает только в редакторе Unity)
        Debug.Log("Игра закрыта");
    }
}
