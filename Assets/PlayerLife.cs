using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerLife : MonoBehaviour
{
    [Header("플레이어 설정")]
    [SerializeField] private int maxLives = 3;
    private static int currentLives;

    [Header("낙사 설정")]
    [SerializeField] private float fallThreshold = -10f;

    [Header("씬 설정")]
    [SerializeField] private string deathSceneName = "DeathScene";

    private Vector3 respawnPoint;
    private Rigidbody2D rb;

    [Header("UI 연결")]
    [SerializeField] private Text livesText;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        currentLives = maxLives;

        respawnPoint = transform.position;
        UpdateLifeUI();
    }

    private void Update()
    {
        if (transform.position.y < fallThreshold)
        {
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
            Die();
        }
        else
        {
            Respawn();
        }
    }

    private void Respawn()
    {
        transform.position = respawnPoint;
        rb.linearVelocity = Vector2.zero;
        Debug.Log("부활! 남은 목숨: " + currentLives);
    }

    private void UpdateLifeUI()
    {
        livesText.text = "남은 목숨: " + currentLives;
    }

    private void Die()
    {
        ItemCollector.ResetCherries();

        SceneManager.LoadScene(deathSceneName);

        Debug.Log("완전히 사망! DeathScene으로 이동합니다.");
    }
}