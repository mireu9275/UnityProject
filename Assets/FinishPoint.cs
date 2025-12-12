using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishPoint : MonoBehaviour
{
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