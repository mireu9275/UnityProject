using UnityEngine;
using UnityEngine.SceneManagement; // 씬 이동을 위해 필수

public class MainMenu : MonoBehaviour
{
    // 게임 시작 버튼에 연결할 함수
    public void PlayGame()
    {
        ItemCollector.ResetCherries();

        // "GameScene" 씬을 불러옵니다.
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