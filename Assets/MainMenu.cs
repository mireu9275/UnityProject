using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        ItemCollector.ResetCherries();

        SceneManager.LoadScene("GameScene");
    }

    public void QuitGame()
    {
        Debug.Log("게임이 종료되었습니다!");

        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}