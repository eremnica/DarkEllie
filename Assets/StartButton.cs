using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour
{
    public void startGame(string StartGame)
    {
        SceneManager.LoadScene(StartGame);
    }
}
