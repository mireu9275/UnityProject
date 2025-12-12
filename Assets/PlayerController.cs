using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 6f;
    public float jumpForce = 12f;

    [Header("Ground Check")]
    public Transform groundCheck;
    public float groundCheckRadius = 0.1f;
    public LayerMask groundLayer;

    private Rigidbody2D rb;
    private Animator anim;
    private bool isGrounded;
    private float inputHorizontal;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        // 입력 처리
        inputHorizontal = Input.GetAxisRaw("Horizontal");

        // 점프 입력: Space, W, ↑ 모두 인식
        bool jumpPressed =
            Input.GetButtonDown("Jump") ||
            Input.GetKeyDown(KeyCode.W) ||
            Input.GetKeyDown(KeyCode.UpArrow);

        if (jumpPressed && isGrounded)
            Jump();

        // 애니메이션 처리
        UpdateAnimation();

        Debug.DrawLine(groundCheck.position, groundCheck.position + Vector3.down * 0.5f, Color.red);


    }

    private void FixedUpdate()
    {
        // 이동
        rb.linearVelocity = new Vector2(inputHorizontal * moveSpeed, rb.linearVelocity.y);

        // 땅 체크
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        // 좌우 반전
        if (inputHorizontal != 0)
        {
            float dir = inputHorizontal > 0 ? 1f : -1f;
            transform.localScale = new Vector3(
                dir * Mathf.Abs(transform.localScale.x),
                transform.localScale.y,
                transform.localScale.z
            );
        }
    }

    void Jump()
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
    }

    void UpdateAnimation()
    {
        anim.SetBool("PlayerRun", Mathf.Abs(inputHorizontal) > 0.1f);
        anim.SetBool("PlayerIdle", isGrounded);

        // 위로 올라가면 Jump, 아래면 Fall
        anim.SetBool("PlayerJump", rb.linearVelocity.y > 0.1f && !isGrounded);
        anim.SetBool("PlayerFall", rb.linearVelocity.y < -0.1f && !isGrounded);
        Debug.Log("isGrounded: " + isGrounded);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }
}
