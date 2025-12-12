using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerLife : MonoBehaviour
{
    [Header("플레이어 설정")]
    [SerializeField] private int maxLives = 3;
    private static int currentLives;

    [Header("낙사 설정")]
    [SerializeField] private float fallThreshold = -10f; // 이 높이보다 떨어지면 죽습니다.

    // 시작 위치를 기억할 변수
    private Vector3 respawnPoint;
    private Rigidbody2D rb;

    [Header("UI 연결")]
    [SerializeField] private Text livesText;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentLives = maxLives;

        // 게임 시작 시 현재 위치 저장
        respawnPoint = transform.position;

        UpdateLifeUI();
    }

    // ★ 추가된 부분: 매 프레임마다 높이를 검사합니다.
    private void Update()
    {
        // 플레이어의 Y 위치가 설정한 값(-10)보다 작아지면
        if (transform.position.y < fallThreshold)
        {
            // 데미지를 입히고 부활(또는 사망) 시킵니다.
            TakeDamage();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Trap"))
        {
            TakeDamage();
        }
    }

    private void TakeDamage()
    {
        currentLives--;
        UpdateLifeUI();

        if (currentLives <= 0)
        {
            Die(); // 목숨 0이면 게임 오버
            ItemCollector.ResetCherries();
        }
        else
        {
            Respawn(); // 목숨 남았으면 부활
        }
    }

    private void Respawn()
    {
        // 1. 위치 이동
        transform.position = respawnPoint;

        // 2. 떨어지던 속도 초기화 (이게 없으면 부활하자마자 다시 쑥 떨어질 수 있음)
        rb.linearVelocity = Vector2.zero;

        Debug.Log("낙사 혹은 함정! 부활했습니다. 남은 목숨: " + currentLives);
    }

    private void UpdateLifeUI()
    {
        livesText.text = "남은 목숨: " + currentLives;
    }

    private void Die()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Debug.Log("게임 오버! 씬을 재시작합니다.");
    }
}