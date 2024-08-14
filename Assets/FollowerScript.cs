using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // For TextMeshPro elements

public class Follower : MonoBehaviour
{
    public float followDistance = 2f;     // Distance within which the follower will start following
    public float followSpeed = 3f;        // Speed of the follower
    private bool isFollowing = false;     // Toggle for following state
    private float stoppingDistance = 1.5f; // Distance at which the follower stops
    private bool isStopped = false;       // Track whether the follower has stopped

    public TextMeshProUGUI proximityMessage;     // Reference to the UI TextMeshPro element for the message

    private Transform activeCharacter;    // Reference to the currently controlled character
    private CharacterSwitcher characterSwitcher;

    private void Start()
    {
        // Find the CharacterSwitcher script in the scene
        characterSwitcher = FindObjectOfType<CharacterSwitcher>();

        // Ensure the proximity message is hidden at the start
        if (proximityMessage != null)
        {
            proximityMessage.gameObject.SetActive(false);
            // Ensure the text is using auto-size for dynamic scaling
            proximityMessage.enableAutoSizing = true;
            proximityMessage.fontSizeMin = 10; // Adjust min size as needed
            proximityMessage.fontSizeMax = 50; // Adjust max size as needed
        }
    }

    void Update()
    {
        if (characterSwitcher != null)
        {
            // Get the active character from the CharacterSwitcher script
            activeCharacter = characterSwitcher.GetActiveCharacter().transform;

            if (activeCharacter != null)
            {
                float distanceToTarget = Vector2.Distance(transform.position, activeCharacter.position);

                // Show/hide the proximity message based on distance
                if (distanceToTarget <= followDistance)
                {
                    if (proximityMessage != null)
                    {
                        proximityMessage.gameObject.SetActive(true); // Show the message
                    }

                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        isFollowing = !isFollowing; // Toggle the following state
                        isStopped = false;          // Reset the stopped state when toggling
                    }
                }
                else
                {
                    if (proximityMessage != null)
                    {
                        proximityMessage.gameObject.SetActive(false); // Hide the message
                    }
                }

                // If following, move towards the target
                if (isFollowing)
                {
                    FollowTarget(distanceToTarget);
                }
            }
        }
    }

    private void FollowTarget(float distanceToTarget)
    {
        // Move towards the active character if outside of the stopping distance
        if (distanceToTarget > stoppingDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, activeCharacter.position, followSpeed * Time.deltaTime);
            isStopped = false; // Ensure the follower is not considered stopped
        }
        else
        {
            // If within stopping distance, stop moving
            isStopped = true;
        }

        // Resume following if stopped and the character moves again
        if (isStopped && distanceToTarget > followDistance)
        {
            isFollowing = true;
        }
    }
}
