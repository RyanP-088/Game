using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// The player controller script that all rats use.
/// </summary>
public class PlayerController : MonoBehaviour
{
    [SerializeField] protected float speed, jumpspeed;
    [SerializeField] protected LayerMask groundLayer;
    [SerializeField] protected LayerMask wallLayer;

    [SerializeField] public int jumpPower;
    [SerializeField] float fallMultiplier;
    [SerializeField] float jumpTime;
    [SerializeField] float jumpMultiplier;
    Vector2 vecGravity;
    public bool isJumping;
    public float jumpCounter;


    protected Animator animator;
    protected PlayerControls playerControls;
    protected Rigidbody2D rb;
    protected Collider2D col;
    protected SpriteRenderer spriteRenderer;
    private Color originalColor;
    protected bool isLookingRight;
    private enum Layers
    {
        Intangible = 8,
        Tangible = 7
    }
    private enum SortingLayers
    {
        Intangible,
        Tangible
    }


    /// <summary>
    /// Checks if the rat is touching a wall.
    /// </summary>
    /// <returns>Returns true if the rat is touching a wall.</returns>
    protected bool onWall()
    {
        return Physics2D.Raycast(transform.position, -transform.up, col.bounds.extents.y + 0.25f, wallLayer);
    }

    /// <summary>
    /// Checks if the rat is touching the ground.
    /// </summary>
    /// <returns>Returns true if the rat is touching the ground.</returns>
    protected bool IsGrounded()
    {
        return Physics2D.Raycast(transform.position, -transform.up, col.bounds.extents.y + 0.25f, groundLayer);
    }

    /// <summary>
    /// Makes the rat jump.
    /// </summary>
    protected virtual void Jump()
    {
        if (IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpPower); // Initiates the jump
            isJumping = true;
            jumpCounter = 0;
        }


    }


    /// <summary>
    /// Movement controls.
    /// </summary>
    protected virtual void Move()
    {
        // Get input value
        float movementInput = playerControls.Grounded.Move.ReadValue<float>();

        // Move the rat
        Vector2 currentPosition = transform.position;
        currentPosition.x += movementInput * speed * Time.deltaTime;
        transform.position = currentPosition;

        // Set animation
        bool isWalking = true && movementInput != 0;
        animator.SetBool("isWalking", isWalking);
        animator.SetBool("offGround", !IsGrounded());

        // Flip the rat's sprite
        if (movementInput > 0)
            isLookingRight = false;
        if (movementInput < 0)
            isLookingRight = true;
        spriteRenderer.flipX = !isLookingRight;

    }

    private void Awake()
    {
        playerControls = new PlayerControls();
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        animator = GetComponent<Animator>();

        vecGravity = new Vector2(0, -Physics2D.gravity.y);///////////////////// 
    }

    private void OnEnable()
    {
        playerControls.Enable();
        transform.position = new Vector3(transform.position.x, transform.position.y, -1f);
        gameObject.layer = (int)Layers.Tangible;
        spriteRenderer.color = originalColor;
        spriteRenderer.sortingOrder = (int)SortingLayers.Tangible;
    }

    private void OnDisable()
    {
        playerControls.Disable();
        transform.SetPositionAndRotation(new Vector3(transform.position.x, transform.position.y, 0f), Quaternion.identity);
        rb.gravityScale = 4f;
        gameObject.layer = (int) Layers.Intangible;
        spriteRenderer.color = new Color(originalColor.r / 2, originalColor.g / 2, originalColor.b / 2, 1f);
        spriteRenderer.sortingOrder = (int) SortingLayers.Intangible;
    }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        //vecGravity = new Vector2(0, -Physics2D.gravity.y);
        playerControls.Grounded.Jump.performed += _ => Jump();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        Move();
        //Debug.Log(playerControls.Grounded.Jump.phase);

        if (rb.velocity.y > 0 && isJumping)
        {

            jumpCounter += Time.deltaTime;

            if (jumpCounter > jumpTime) isJumping = false;

            float t = jumpCounter / jumpTime;
            float currentJumpM = jumpMultiplier;

            if (t > 0.5f)
            {
                currentJumpM = jumpMultiplier * (1 - t);
            }

            rb.velocity += vecGravity * currentJumpM * Time.deltaTime;
           
        }
        else
        {
            isJumping = false; // Stop the jump if the button is released or time limit reached
        }
        

        if (rb.velocity.y < 0)
        {
            //rb.velocity -= vecGravity * fallMultiplier * Time.deltaTime; // Apply gravity when falling
        }

        //button is released
        if (isJumping && playerControls.Grounded.Jump.ReadValue<float>() == 0)
        {
            isJumping = false;
            jumpCounter = 0;

            if (rb.velocity.y > 0)
            {
                rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.6f);
            }
        }

    }

}
