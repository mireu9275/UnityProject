using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathMenu : MonoBehaviour
{
    public void RestartGame()
    {
        ItemCollector.ResetCherries();

        SceneManager.LoadScene("GameScene");
    }

    public void GoToMainMenu()
    {
        ItemCollector.ResetCherries();

        SceneManager.LoadScene("MainScene");
    }
}