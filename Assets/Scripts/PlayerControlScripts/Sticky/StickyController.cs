using UnityEngine;

/// <summary>
/// The sticky contoller.
/// </summary>
public class StickyContoller : PlayerController
{
    private float currentAngle = 0f;
    private bool moving = false;
    [SerializeField]
    private float unstickTime = .75f;
    private float unstickTimer = 10f;

    /// <summary>
    /// Functionality for the sticky rat to stick to the wall
    /// </summary>
    private void StickToWall()
    {
        if (!moving) return;

        // Sets a vector representing where the rat is currently looking
        int verticalRight = isLookingRight ? 1 : -1;
        Vector2 directionVector = verticalRight * transform.right;

        // Checks if rat is touching a wall
        RaycastHit2D hit = Physics2D.Raycast(transform.position, directionVector, col.bounds.extents.x + 0.1f, wallLayer);


        // Rotates the rat if touching a wall
        if (hit && (unstickTimer >= unstickTime))
        {
            // Rotate the rat
            transform.Rotate(0, 0, verticalRight * 90f);
            currentAngle = transform.rotation.eulerAngles.z;

            // Prevent constant rotation
            unstickTimer = 0f;

            // Prevents slipping
            rb.velocity = Vector2.zero;
        }
        // Resets rotation in the air
        else if (!IsGrounded())
        {
            currentAngle = 0;
            transform.rotation = Quaternion.identity;
        }
    }


    /// <summary>
    /// Movement controls.
    /// </summary>
    protected override void Move()
    {
        // Get input value
        float movementInput = playerControls.Grounded.Move.ReadValue<float>();

        // Move the rat
        Vector3 movementVector = transform.right;
        movementVector = movementInput * speed * Time.deltaTime * movementVector;
        transform.position += movementVector;

        // Set animation
        moving = true && movementInput != 0;
        animator.SetBool("isWalking", moving);
        animator.SetBool("offGround", !IsGrounded());

        // Flip the rat's sprite
        if (movementInput > 0)
            isLookingRight = true;
        if (movementInput < 0)
            isLookingRight = false;
        spriteRenderer.flipX = !isLookingRight;

        StickToWall();
    }

    protected override void Jump()
    {
        if (IsGrounded())
        {
            if (Vector3.up == transform.up)
                rb.AddForce(transform.up * jumpspeed, ForceMode2D.Impulse);
            else
            {
                //Debug.Log("sideways jump");
                rb.AddForce((Vector3.up + transform.up).normalized * (jumpspeed / 1.5f), ForceMode2D.Impulse);
            }
        }

    }




    protected override void Start()
    {
        base.Start();
        currentAngle = transform.rotation.z;
        rb.gravityScale = 0f;
    }

    protected override void Update()
    {
        // Increment time since last rotation
        unstickTimer += Time.deltaTime;

        base.Update();

        rb.gravityScale = 0f;
        // Simulate gravity manually
        rb.AddForce(Physics2D.gravity.magnitude * -transform.up);
    }
}