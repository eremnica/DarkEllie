using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSwitcher : MonoBehaviour
{
    public GameObject[] characters;   // Array to store all characters
    private int currentCharacterIndex; // Index of the currently active character
    public CameraFollow cameraFollow;  // Reference to the CameraFollow script

    void Start()
    {
        // Ensure only the first character is active initially
        currentCharacterIndex = 0;
        ActivateCharacter(currentCharacterIndex);
    }

    void Update()
    {
        // Switch character when the Tab key is pressed
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            SwitchCharacter();
        }
    }

    private void SwitchCharacter()
    {
        // Deactivate the current character
        characters[currentCharacterIndex].GetComponent<Hero>().enabled = false;

        // Move to the next character in the array
        currentCharacterIndex = (currentCharacterIndex + 1) % characters.Length;

        // Activate the new character
        ActivateCharacter(currentCharacterIndex);
    }

    private void ActivateCharacter(int index)
    {
        for (int i = 0; i < characters.Length; i++)
        {
            bool isActive = i == index;
            var heroScript = characters[i].GetComponent<Hero>();
            if (heroScript != null)
            {
                heroScript.enabled = isActive;
            }

            // Check if the character has a SpriteRenderer
            var spriteRenderer = characters[i].GetComponent<SpriteRenderer>();
            if (spriteRenderer != null)
            {
                spriteRenderer.color = isActive ? Color.white : Color.gray; // Optional: visually differentiate inactive characters
            }
        }

        // Update the camera to follow the newly activated character
        cameraFollow.target = characters[index].transform;
    }

    // New method to get the currently active character
    public GameObject GetActiveCharacter()
    {
        return characters[currentCharacterIndex];
    }
}