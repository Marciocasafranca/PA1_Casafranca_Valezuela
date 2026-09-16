using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 7f;
    public float jumpForce = 10f;

    public ParticleSystem jumpDust;

    private Rigidbody2D rb;
    private Animator animator;
    private AudioSource audioSource;
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        audioSource.playOnAwake = false;
        audioSource.spatialBlend = 0f;

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

            PlayJumpSound();
        }
    }

    private void PlayJumpSound()
    {
        int sampleRate = 44100;
        float duration = 0.20f;
        int samples = Mathf.CeilToInt(sampleRate * duration);

        AudioClip clip = AudioClip.Create(
            "JumpSFX",
            samples,
            1,
            sampleRate,
            false
        );

        float[] data = new float[samples];

        for (int i = 0; i < samples; i++)
        {
            float t = i / (float)sampleRate;
            float frequency = Mathf.Lerp(300f, 750f, t / duration);
            float volume = 0.25f * (1f - t / duration);

            data[i] = Mathf.Sin(2f * Mathf.PI * frequency * t) * volume;
        }

        clip.SetData(data, 0);
        audioSource.PlayOneShot(clip);
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