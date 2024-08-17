using UnityEngine;

public class CameraFollow : MonoBehaviour

{
    public Transform target;  // Reference to the player's transform
    public float offsetX = 0f; // Horizontal offset


    private float fixedY;      // Fixed Y position for the camera

    private void Start()
    {
        // Initialize the fixed Y position with the camera's initial Y position
        fixedY = transform.position.y;

        // Optionally, you can also initialize offsetX based on the initial position
        offsetX = transform.position.x - target.position.x;
    }

    private void LateUpdate()
    {
        // Follow the character's X movement, keeping the Y position fixed
        transform.position = new Vector3(target.position.x + offsetX, fixedY, transform.position.z);
    }

}

