using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishPoint : MonoBehaviour
{
    // ★ 인스펙터에서 직접 '이동할 씬 이름'을 적을 수 있게 변수를 만듭니다.
    [Header("이동 설정")]
    [SerializeField] private string targetSceneName;

    private bool levelCompleted = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !levelCompleted)
        {
            levelCompleted = true;

            Invoke("CompleteLevel", 0.5f);
        }
    }

    private void CompleteLevel()
    {
        SceneManager.LoadScene(targetSceneName);
    }
}