using UnityEngine;
using UnityEngine.SceneManagement;

public class ClearMenu : MonoBehaviour
{
    public void GoToMainMenu()
    {
        ItemCollector.ResetCherries();

        SceneManager.LoadScene("MainScene");
    }
}