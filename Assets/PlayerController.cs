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

    private float defaultJumpForce;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        defaultJumpForce = jumpForce;
    }

    private void Update()
    {
        inputHorizontal = Input.GetAxisRaw("Horizontal");

        bool jumpPressed =
            Input.GetButtonDown("Jump") ||
            Input.GetKeyDown(KeyCode.W) ||
            Input.GetKeyDown(KeyCode.UpArrow);

        if (jumpPressed && isGrounded)
            Jump();

        UpdateAnimation();

        Debug.DrawLine(groundCheck.position, groundCheck.position + Vector3.down * 0.5f, Color.red);
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(inputHorizontal * moveSpeed, rb.linearVelocity.y);

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

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

        anim.SetBool("PlayerJump", rb.linearVelocity.y > 0.1f && !isGrounded);
        anim.SetBool("PlayerFall", rb.linearVelocity.y < -0.1f && !isGrounded);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("JumpPad"))
        {
            jumpForce = defaultJumpForce + 3f;
            Debug.Log("점프력 증가! 현재 점프력: " + jumpForce);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("JumpPad"))
        {
            jumpForce = defaultJumpForce;
            Debug.Log("점프력 복구. 현재 점프력: " + jumpForce);
        }
    }
}