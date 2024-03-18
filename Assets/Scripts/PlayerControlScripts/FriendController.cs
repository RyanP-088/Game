using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;
using Pathfinding;
using System.Numerics;
using Vector3 = UnityEngine.Vector3;
using System.Linq;
using UnityEngine.UIElements;

public class FriendController : MonoBehaviour
{
    [Header("Pathfinding")]
    [SerializeField] private Transform target;

    [SerializeField] private Vector2 offset = new Vector2(0, 0);
    [SerializeField] private float maxDistance = 50f;
    [SerializeField] private float minDistance = 0f;
    [SerializeField] private float pathUpdateSeconds = 0.5f;

    [Header("Physics")]
    [SerializeField] private LayerMask collisionLayer;
    [SerializeField] private float speed = 200f;
    [SerializeField] private float nextWaypointDistance = 3f;
    [SerializeField] private float jumpNodeHeightRequirement = 0.8f;
    [SerializeField] private float jumpSpeed = 1f;
    [SerializeField] private float jumpCheckOffset = 0.1f;

    [Header("Custom Behavior")]
    [SerializeField] private bool teleportEnabled = false;
    [SerializeField] private float teleportDistance;
    [SerializeField] private float teleportAfter = 5f;
    [SerializeField] private bool followEnabled = true;
    [SerializeField] private bool jumpEnabled = true;

    private Path path;
    private int currentWaypoint = 0;
    bool IsGrounded;
    private Seeker seeker;
    private Rigidbody2D rb;
    private float teleportTimer;
    private SpriteRenderer spriteRenderer;
    private bool isLookingRight;
    private Animator animator;
    private bool isWalking = false;

    public void SetFollowEnabled(bool b) => this.followEnabled = b;

    public bool GetFollowEnabled() => this.followEnabled;

    public void SetJumpEnabled(bool b) => this.jumpEnabled = b;

    public bool GetJumpEnabled() => this.jumpEnabled;

    public void SetTarget(Transform target) => this.target = target;

    public void SetTargetOffset(Vector2 offset) => this.offset = offset;

    public void SetTeleportEnabled(bool b)
    {
        this.teleportEnabled = b;
        teleportTimer = 0f;
    }

    public bool GetTeleportEnabled() => this.teleportEnabled;

    public void SetTeleportTime(float time) => this.teleportAfter = time;

    public void SetTeleportDistance(float d) => this.teleportDistance = d;

    public void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        animator = GetComponent<Animator>();

        InvokeRepeating("UpdatePath", 1f, pathUpdateSeconds);
    }

    public void FixedUpdate()
    {
        if (TargetInRange() && followEnabled)
            PathFollow();
    }

    private void UpdatePath()
    {
        if (/*TargetInRange() && */followEnabled && seeker.IsDone())
        {
            currentWaypoint = 0;
            seeker.StartPath(rb.position, target.position + (Vector3)offset, OnPathComplete);
        }   
    }

    private void PathFollow()
    {
        if (path == null)
            return;
        
        if (currentWaypoint >= path.vectorPath.Count)
            return;

        IsGrounded = Physics2D.Raycast(transform.position, Vector2.down, GetComponent<Collider2D>().bounds.extents.y + jumpCheckOffset, collisionLayer);

        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;

        if (jumpEnabled && IsGrounded && direction.y > jumpNodeHeightRequirement)
            rb.AddRelativeForce(new Vector2(rb.velocity.x, jumpSpeed), ForceMode2D.Impulse);

        Vector2 originalPosition = transform.position;
        Vector2 currentPosition = originalPosition;
        currentPosition.x += direction.x * speed * Time.deltaTime;
        transform.position = currentPosition;

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);
        if (distance < nextWaypointDistance)
            currentWaypoint++;

        //Debug.Log("Path followed a little");
    }

    private bool TargetInRange()
    {
        float distance = Vector2.Distance(transform.position, target.position);
        return distance >= minDistance && distance <= maxDistance;
    }

    private void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    private void MoveHorizontally(Vector3 waypoint)
    {
        isWalking = false;
        int direction;
        float hDistance = waypoint.x - transform.position.x;
        if (hDistance < minDistance)
            return;

        if (hDistance > 0)
        {
            //Debug.Log("moving right");
            isLookingRight = true;
            direction = 1;
        }
        else
        {
            //Debug.Log("moving left");
            isLookingRight = false;
            direction = -1;
        }
        isWalking = true;

        animator.SetBool("isWalking", isWalking);
        animator.SetBool("offGround", !IsGrounded);

        Vector2 currentPosition = transform.position;
        currentPosition.x += direction * speed * Time.deltaTime;
        transform.position = currentPosition;
    }

    public void Teleport()
    {
        if (teleportEnabled)
        {
            Vector2 teleportLocation = target.position + (Vector3) (offset + new Vector2(0f, 0.2f));

            bool hit = Physics2D.CapsuleCast(teleportLocation, GetComponent<Collider2D>().bounds.size, CapsuleDirection2D.Vertical, 0, Vector2.down, GetComponent<Collider2D>().bounds.extents.y, collisionLayer);

            if (!hit)
            {
                transform.position = teleportLocation;
                rb.velocity = Vector2.zero;
                teleportTimer = 0;
                //Debug.Log("Successfully teleported to " + (Vector2)teleportLocation);
            }
        }
    }

    public void Update()
    {
        if (teleportEnabled)
        {
            if (Vector2.Distance(transform.position, target.position) > teleportDistance)
                teleportTimer += Time.deltaTime;
            else
                teleportTimer = 0;
            
            if (teleportTimer >= teleportAfter)
                Teleport();
        }

        if (TargetInRange() && followEnabled)
        {
            if (path == null)
                return;
            if (currentWaypoint >= path.vectorPath.Count)
                currentWaypoint = path.vectorPath.Count - 1;
            float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);
            if (distance < nextWaypointDistance && currentWaypoint != path.vectorPath.Count - 1)
                currentWaypoint++;
            MoveHorizontally(path.vectorPath[currentWaypoint]);
        }

        if (isLookingRight)
        {
            if (spriteRenderer.name == "dude (sticky)")
            {
                spriteRenderer.flipX = false;
                isLookingRight = true;
            }
            else
            {
                spriteRenderer.flipX = true;
                isLookingRight = false;
            }

        }
        else if (!isLookingRight)
        {
            if (spriteRenderer.name == "dude (sticky)")
            {
                spriteRenderer.flipX = true;
                isLookingRight = false;
            }
            else
            {
                spriteRenderer.flipX = false;
                isLookingRight = true;
            }
        }
    }
}
