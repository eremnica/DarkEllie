using JetBrains.Annotations;
using UnityEngine;

/// <summary>
/// Represents a Hero character in the game with movement, jumping, and respawn functionality.
/// </summary>
public class Hero : MonoBehaviour
{
    [SerializeField]
    public float speed = 3f; // Movement speed of the hero.
    public float jumpForce = 10f; // Force applied when the hero jumps.
    public float respawnYThreshold = -10f; // Y position threshold for respawning the hero.
    public Vector2 respawnPosition; // Position to respawn the hero after falling below the threshold.
    public Rigidbody2D rb; // Reference to the Rigidbody2D component.
    public SpriteRenderer sprite; // Reference to the SpriteRenderer component.
    public float rayDistance = 12.2f;
    public Vector2 boxSize;
    public float castDistance;
    public LayerMask groundLayer;

    /// <summary>
    /// Initializes the hero's components and sets the respawn position.
    /// </summary>

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        respawnPosition = transform.position;
    }

    /// <summary>
    /// Called once per frame to handle movement, jumping, and respawning.
    /// </summary>
    private void Update()
    {
        //CheckGround();
        HandleMovement();
        HandleRespawn();
    }

    /// <summary>
    /// Handles horizontal movement of the hero based on player input.
    /// </summary>
    private void HandleMovement()
    {
        Run();
        Jump();
    }

    /// <summary>
    /// Checks if the hero has fallen below the respawn threshold and respawns if necessary.
    /// </summary>
    ///
    /// <summary>
    /// Moves the hero horizontally based on player input.
    /// </summary>
    private void Run()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        rb.linearVelocity = new Vector2(horizontalInput * speed, rb.linearVelocity.y);
        sprite.flipX = horizontalInput < 0;
    }

    /// <summary>
    /// Applies an upward force to the hero to make it jump.
    /// </summary>
    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded())
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    /// <summary>
    /// Checks if the hero is grounded by detecting collisions with the ground.
    /// </summary>
    //private void CheckGround()
    //{
    //  RaycastHit2D hit = Physics2D.Raycast(rb.position, Vector2.down, rayDistance, LayerMask.GetMask("Ground"));

    //if (hit.collider != null)
    //{
    //  isGrounded = true;
    //Debug.Log("Grounded");
    //}
    //else
    //{
    //  isGrounded = false;
    //}

    //isGrounded = Physics2D.OverlapCircle(transform.position, 0.1f, LayerMask.GetMask("Ground")) != null;

    // }
    private bool isGrounded()
    {
        if (Physics2D.BoxCast(transform.position, boxSize, 0, -transform.up, castDistance, groundLayer))
        {
            return true;

        }
        else
        {
            return false;

        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position - transform.up * castDistance, boxSize);
    }

    private void HandleRespawn()
    {
        if (transform.position.y < respawnYThreshold)
        {
            // Reset the position to the respawn position.
            transform.position = respawnPosition;
            // Reset the rotation to zero (or any desired rotation).
            transform.rotation = Quaternion.identity;
            // Reset the Rigidbody's velocity to prevent carrying over any momentum.
            rb.linearVelocity = Vector2.zero;
            // Reset the Rigidbody's angular velocity to prevent any unwanted spinning.
            rb.angularVelocity = 0f;
        }
    }
}
