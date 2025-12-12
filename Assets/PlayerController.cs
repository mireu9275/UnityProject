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

    // 원래 설정된 점프력을 기억하기 위한 변수
    private float defaultJumpForce;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        // 게임 시작 시 설정된 기본 점프력을 저장해둡니다 (12f)
        defaultJumpForce = jumpForce;
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
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }

    // ---------------------------------------------------------
    // 여기서부터 추가된 부분입니다 (충돌 감지)
    // ---------------------------------------------------------

    // 발판에 닿았을 때 (발판 위에 섰을 때)
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 닿은 물체의 태그가 "JumpPad"라면
        if (collision.gameObject.CompareTag("JumpPad"))
        {
            // 점프력을 원래 값 + 3으로 설정
            jumpForce = defaultJumpForce + 3f;
            Debug.Log("점프력 증가! 현재 점프력: " + jumpForce);
        }
    }

    // 발판에서 떨어졌을 때 (점프해서 공중으로 가거나, 밖으로 걸어나갔을 때)
    private void OnCollisionExit2D(Collision2D collision)
    {
        // "JumpPad"에서 떨어졌다면
        if (collision.gameObject.CompareTag("JumpPad"))
        {
            // 점프력을 다시 원래대로 복구
            jumpForce = defaultJumpForce;
            Debug.Log("점프력 복구. 현재 점프력: " + jumpForce);
        }
    }
}