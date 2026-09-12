using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 7f;
    public float jumpForce = 5f;

    private Rigidbody2D rb;
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float move = Input.GetAxisRaw("Horizontal");

        rb.linearVelocity = new Vector2(
            move * speed,
            rb.linearVelocity.y
        );

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.linearVelocity = new Vector2(
                rb.linearVelocity.x,
                jumpForce
            );
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (
            collision.gameObject.CompareTag("Ground") ||
            collision.gameObject.CompareTag("Platform")
        )
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (
            collision.gameObject.CompareTag("Ground") ||
            collision.gameObject.CompareTag("Platform")
        )
        {
            isGrounded = false;
        }
    }
}