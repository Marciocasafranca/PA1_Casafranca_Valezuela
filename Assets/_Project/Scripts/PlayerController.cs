using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 7f;
    public float jumpForce = 10f;

    public ParticleSystem jumpDust;

    private Rigidbody2D rb;
    private Animator animator;
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        if (jumpDust != null)
        {
            jumpDust.Stop();
        }
    }

    void Update()
    {
        float move = Input.GetAxisRaw("Horizontal");

        rb.linearVelocity = new Vector2(
            move * speed,
            rb.linearVelocity.y
        );

        animator.SetBool("IsRunning", move != 0);
        animator.SetBool("IsGrounded", isGrounded);

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.linearVelocity = new Vector2(
                rb.linearVelocity.x,
                jumpForce
            );

            animator.SetTrigger("Jump");

            if (jumpDust != null)
            {
                jumpDust.Play();
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (
            collision.gameObject.CompareTag("Ground") ||
            collision.gameObject.CompareTag("Platform") ||
            collision.gameObject.name == "PushableBox"
        )
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (
            collision.gameObject.CompareTag("Ground") ||
            collision.gameObject.CompareTag("Platform") ||
            collision.gameObject.name == "PushableBox"
        )
        {
            isGrounded = false;
        }
    }
}