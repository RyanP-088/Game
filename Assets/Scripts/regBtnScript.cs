using System;
using UnityEngine;

public class ButtonScript : MonoBehaviour
{
    [SerializeField] private GameObject doorObj;
    [SerializeField] private GameObject platObj;
    [SerializeField] private PlatformScript platScript;
    [SerializeField] private bool forDoor;
    public Vector3 depressedPositionOffset = new Vector3(0, -0.1f, 0);
    // This time we will use a non-static variable to track the press count for each individual button.
    private int pressCount = 0;
    public bool pressed = false;

    //button visual
    SpriteRenderer spriteRenderer;
    public Sprite buttonUp;
    public Sprite buttonDown;

    private void Start()
    {
        if (forDoor && doorObj == null)
        {
            Debug.LogError("Door object is not assigned to the button script.", this);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Enemy") || other.CompareTag("Weight"))
        {
            pressCount++;
            if (forDoor && pressCount == 1) // Only act if the button is for a door and it's the first press
            {
                doorObj.SetActive(false); // Open the door
                transform.position += depressedPositionOffset; // Depress button visually
                pressed = true;
            }
            else if (!forDoor && pressCount == 1)
            {

                // If it's for a platform, just activate the platform
                transform.position += depressedPositionOffset; // Depress button visually
                platScript.activateMove();
                pressed = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Enemy") || other.CompareTag("Weight"))
        {
            pressCount--;
            if (forDoor && pressCount == 0) // Only act if the button is for a door and there's no more press
            {
                // Before we close the door, check if any other button is still pressed
                if (!AnyOtherButtonPressed())
                {
                    doorObj.SetActive(true); // Close the door
                }
                transform.position -= depressedPositionOffset; // Reset button visual state
                pressed = false;
            }
            else if (!forDoor && pressCount == 0)
            {
                // If it's for a platform, deactivate the platform
                if (!AnyOtherButtonPressed())
                {
                    platScript.deactivateMove();
                }
                transform.position -= depressedPositionOffset; // Reset button visual state
                pressed = false;
            }
        }
    }

    private bool AnyOtherButtonPressed()
    {
        // Get all button scripts in the scene
        ButtonScript[] allButtons = FindObjectsOfType<ButtonScript>();
        //know this isn't very efficienet but figure we won't have that many buttons in each scene to search
        foreach (ButtonScript button in allButtons)
        {
            // Check if any other button that controls the same door is still pressed
            if (button != this && button.pressCount > 0 && button.doorObj == this.doorObj && button.forDoor)
            {
                return true; // Another button is still controlling the same door
            }
        }
        return false; // No other button is pressed for this door
    }

    public bool getPressed()
    {
        return pressed;
    }
}
