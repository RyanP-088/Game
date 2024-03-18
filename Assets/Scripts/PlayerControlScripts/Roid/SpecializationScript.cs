using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SpecializationScript : MonoBehaviour
{
    public GameObject particlePrefab;
    public bool inRangeBreak = false;
    public bool inRangeWeight = false;
    public bool holding = false;
    public bool holdable = true;
    private int counter = 0;
    private bool isInteractButtonPressed = false;

    [SerializeField] GameObject wall;
    [SerializeField] GameObject weight;
    [SerializeField] SpriteRenderer sprite;
    Rigidbody2D rb;

    private PlayerControls controls;

    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
        controls = new PlayerControls();
        //controls.Grounded.Interact.performed += _ => isInteractButtonPressed = true;
        //controls.Grounded.Interact.canceled += _ => isInteractButtonPressed = false;
    }

    private void OnEnable()
    {
        controls.Grounded.Enable();
    }

    private void OnDisable()
    {
        controls.Grounded.Disable();
    }
    private void FixedUpdate()
    {
        counter++;
        if (counter > 1000000) counter = 1;

        if (inRangeWeight && controls.Grounded.Interact.IsPressed() && !holding && holdable)
        {
            holdable = false;
            holding = true;
            isInteractButtonPressed = false;

            rb.bodyType = RigidbodyType2D.Kinematic;
            counter = 0;
        } else if (!controls.Grounded.Interact.IsPressed())
        {
            holdable = true;
        }

        if (!controls.Grounded.Interact.IsPressed() && !inRangeBreak && holding && counter >= 15)
        {
            holding = false;
            isInteractButtonPressed = false;

            if (sprite.flipX == true)
            {
                weight.transform.position = new Vector3(transform.position.x + 1.10f, transform.position.y + .75f, weight.transform.position.z);
            }
            else
            {
                weight.transform.position = new Vector3(transform.position.x - 1.10f, transform.position.y + .75f, weight.transform.position.z);
            }
            rb.bodyType = RigidbodyType2D.Dynamic;
        }

        if (inRangeBreak && controls.Grounded.Interact.IsPressed())
        {
            Instantiate(particlePrefab, transform.position, Quaternion.identity);
            Object.Destroy(wall);
        }

        if (holding == true)
        {
            weight.transform.position = new Vector3(transform.position.x, transform.position.y + 1.5f, weight.transform.position.z);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Interactable")
        {
            inRangeBreak = true;
            wall = collision.gameObject;
        }
        else if (collision.gameObject.tag == "Weight")
        {
            Debug.Log("Weight Trigger");
            inRangeWeight = true;
            weight = collision.gameObject;
            rb = weight.GetComponent<Rigidbody2D>();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Interactable")
        {
            inRangeBreak = false;
            wall = null;
        }
        else if (collision.gameObject.tag == "Weight")
        {
            rb.bodyType = RigidbodyType2D.Dynamic;
            inRangeWeight = false;
            weight = null;
            rb = null;
        }
    }
}
