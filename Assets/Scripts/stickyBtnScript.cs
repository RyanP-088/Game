using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stickyButtonScript : MonoBehaviour
{
    [SerializeField] GameObject doorObj;
    private bool isPressed = false;
    public Vector3 stickydepressedPositionOffset = new Vector3(0, -0.1f, 0);  // Offset when button is pressed. Adjust based on your sprite size.
    SpriteRenderer spriteRenderer;
    public Sprite newSpriteAsset;


    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //might need to add another "or" condition in here for if a box is pushed on top etc.
        if (!isPressed && other.CompareTag("Player") || other.CompareTag("Enemy") || other.CompareTag("Weight"))
        {
            isPressed = true;
           // transform.position += stickydepressedPositionOffset;  // Move the button downwards
            spriteRenderer.sprite = newSpriteAsset;
            doorObj.SetActive(false);
            //door.OpenDoor();  // Call the function to open the door
        }
    }

    public bool getPressed()
    {
        return isPressed;
    }

}


/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stickyButtonScript : MonoBehaviour
{
    [SerializeField] GameObject doorObj;
    private bool isPressed = false;
    public Vector3 stickydepressedPositionOffset = new Vector3(0, -0.1f, 0);  // Offset when button is pressed. Adjust based on your sprite size.
    SpriteRenderer spriteRenderer;
    public Sprite newSpriteAsset;

    private void OnTriggerEnter2D(Collider2D other)
    {
        //might need to add another "or" condition in here for if a box is pushed on top etc.
        if (!isPressed && other.CompareTag("Player"))
        {
            isPressed = true;
            //transform.position += stickydepressedPositionOffset;  // Move the button downwards
            //change button asset
            spriteRenderer.sprite = newSpriteAsset;

            doorObj.SetActive(false);
            //door.OpenDoor();  // Call the function to open the door
        }
    }

}
*/