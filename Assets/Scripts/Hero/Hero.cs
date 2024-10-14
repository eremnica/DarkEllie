using UnityEngine;

public class Hero : MonoBehaviour
{
    [SerializeField]
    public float speed = 3f; // Movement speed of the hero.
    public float jumpForce = 10f; // Force applied when the hero jumps.
    public float respawnYThreshold = -10f; // Y position threshold for respawning the hero.
    public Vector2 respawnPosition; // Position to respawn the hero after falling below the threshold.
    public Rigidbody2D rb; // Reference to the Rigidbody2D component.
    public SpriteRenderer sprite; // Reference to the SpriteRenderer component.
    public float castDistance = 0.5f; // Distance for ground detection.
    public Vector2 boxSize = new Vector2(0.5f, 0.2f); // Size of the box used for ground detection.
    public LayerMask groundLayer; // Ground layer for ground detection.

    private Animator animator; // Reference to the Animator component.

    private void Awake()
    {
        // Initialize the components
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        respawnPosition = transform.position;
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        HandleMovement();
        HandleRespawn();

        float move = Input.GetAxis("Horizontal");

        // Обновление параметра в аниматоре для переключения между Idle и Walk
        if (animator != null) // Проверка на наличие аниматора
        {
            animator.SetBool("isMoving", move != 0); // Если движение есть, включаем ходьбу
        }
    }


    private void HandleMovement()
    {
        Run();
        Jump();
    }

    private void Run()
    {
        float horizontalInput = Input.GetAxis("Horizontal");

        // Move the character
        rb.linearVelocity = new Vector2(horizontalInput * speed, rb.linearVelocity.y);


        // Flip the sprite based on the direction of movement
        if (horizontalInput != 0)
        {
            sprite.flipX = horizontalInput < 0;
        }
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded())
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    private bool isGrounded()
    {
        // Cast a box to check if the hero is on the ground
        return Physics2D.BoxCast(transform.position, boxSize, 0, Vector2.down, castDistance, groundLayer);
    }

    private void HandleRespawn()
    {
        if (transform.position.y < respawnYThreshold)
        {
            // Reset the position to the respawn position
            transform.position = respawnPosition;
            transform.rotation = Quaternion.identity;
            rb.linearVelocity = Vector2.zero; // Reset the velocity
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position - transform.up * castDistance, boxSize);
    }
}
