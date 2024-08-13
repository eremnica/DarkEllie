using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour
{
    [SerializeField] private float speed = 3f;
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private float respawnYThreshold = -10f;  // Y position threshold for respawning
    [SerializeField] private Vector3 respawnPosition;  // Position to respawn the character

    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    private bool isGrounded = false;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponentInChildren<SpriteRenderer>();
         // Optionally, set the respawn position to the initial position of the character
        respawnPosition = transform.position;
    }

    private void FixedUpdate()
    {
        CheckGround();
    }

    private void Update()
    {
        if (Input.GetButton("Horizontal"))
            Run();
         if (isGrounded && Input.GetKeyDown(KeyCode.Space)) // Use GetKeyDown for a fixed jump
            Jump();
              // Check if the character has fallen below the respawn threshold
        if (transform.position.y < respawnYThreshold)
            Respawn();
    }

    private void Run()
    {
        Vector3 dir = transform.right * Input.GetAxis("Horizontal");
        transform.position = Vector3.MoveTowards(transform.position, transform.position + dir, speed * Time.deltaTime);

        sprite.flipX = dir.x < 0.0f;
    }

    private void Jump()
    {
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }

    private void CheckGround()
    {
        Collider2D[] collider = Physics2D.OverlapCircleAll(transform.position, 0.2f);
        isGrounded = collider.Length > 0;
           Debug.Log("Grounded: " + isGrounded);
    foreach (var col in collider)
    {
        Debug.Log("Collided with: " + col.name);
    }
    }
     // Respawn the character to the designated respawn position
    private void Respawn()
    {
    // Reset the position to the respawn position
    transform.position = respawnPosition;

    // Reset the rotation to zero (or any desired rotation)
    transform.rotation = Quaternion.identity;

    // Reset the Rigidbody's velocity to prevent carrying over any momentum
    rb.velocity = Vector2.zero;

    // Reset the Rigidbody's angular velocity to prevent any unwanted spinning
    rb.angularVelocity = 0f;
    }
}
