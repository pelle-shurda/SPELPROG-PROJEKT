using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    private Animator anim;

    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpForce = 10f; // bättre namn än jumpHeight
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float groundCheckRadius = 0.1f; // för enkel justering
    [SerializeField] private AudioSource jumpSound;

    private enum MovementState { idle, running, jumping, falling, doublejumping }
    private MovementState state = MovementState.idle;

    private float horizontalInput;
    private bool isGrounded;
    private int jumpCount;
    private int maxJumps = 2;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        // --- Horisontell rörelse ---
        horizontalInput = Input.GetAxisRaw("Horizontal"); // Raw ger mer direkt input
        rb.linearVelocity = new Vector2(horizontalInput * speed, rb.linearVelocity.y);

        // --- Sprite flip ---
        if (horizontalInput < 0) sprite.flipX = true;
        else if (horizontalInput > 0) sprite.flipX = false;

        // --- Markkontroll ---
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        // Återställ hopp när spelaren landar
        if (isGrounded)
        {
            jumpCount = 0;
        }

        // --- Hoppa / dubbelhopp ---
        if (Input.GetKeyDown(KeyCode.Space) && jumpCount < maxJumps)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            jumpCount++;
            jumpSound.Play();
        }

        // --- Uppdatera animation ---
        UpdateAnimationState();
        anim.SetInteger("state", (int)state);
    }

    private void UpdateAnimationState()
    {
        if (rb.linearVelocity.y > 0.1f)
        {
            state = (jumpCount > 1) ? MovementState.doublejumping : MovementState.jumping;
        }
        else if (rb.linearVelocity.y < -0.1f)
        {
            state = MovementState.falling;
        }
        else
        {
            state = (Mathf.Abs(rb.linearVelocity.x) > 0.1f) ? MovementState.running : MovementState.idle;
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }
}
