using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Movement mechanics variables
    public float maxSpeed = 3.4f;
    public float jumpHeight = 20.0f;
    public float gravityScale = 1.5f;

    public Camera mainCamera;

    float moveDirection = 0; // 0 is still, -1 is left, 1 is right
    bool isGrounded = false;
    Vector3 cameraPos;

    // Object components
    Rigidbody2D r2d;
    CapsuleCollider2D mainCollider;
    Transform t;

    // Use this for initialization
    void Start()
    {
        // initialize object component variables
        t = transform;
        r2d = GetComponent<Rigidbody2D>();
        mainCollider = GetComponent<CapsuleCollider2D>();

        // If freezeRotation is enabled, the rotation in Z is not modified by the physics simulation.
        r2d.freezeRotation = true;

        // Ensures that all collisions are detected when a Rigidbody2D moves.
        r2d.collisionDetectionMode = CollisionDetectionMode2D.Continuous;

        r2d.gravityScale = gravityScale;

        if (mainCamera)
        {
            cameraPos = mainCamera.transform.position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Movement controls
        if ((Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow)) && (isGrounded || Mathf.Abs(r2d.velocity.x) > 0.01f))
        {
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                moveDirection = -1;
            }
            else
            {
                moveDirection = 1;
            }
        }
        else
        {
            if (isGrounded || r2d.velocity.magnitude < 0.01f)
            {
                moveDirection = 0;
            }
        }

        // Jumping
        if (Input.GetKeyDown(KeyCode.UpArrow) && isGrounded)
        {
            // Apply movement velocity in the y direction
            r2d.velocity = new Vector2(r2d.velocity.x, jumpHeight);
        }

        // Assigned Camera follows player
        if (mainCamera)
        {
            mainCamera.transform.position = new Vector3(t.position.x, cameraPos.y, cameraPos.z);
        }
    }

    void FixedUpdate()
    {
        // Get information from player's collider
        Bounds colliderBounds = mainCollider.bounds;
        float colliderRadius = mainCollider.size.x * 0.4f * Mathf.Abs(transform.localScale.x);

        // Check if player is grounded
        Vector3 groundCheckPos = colliderBounds.min + new Vector3(colliderBounds.size.x * 0.5f, colliderRadius * 0.9f, 0);
        
        //Check if any of the overlapping colliders are not player collider, if so, set isGrounded to true
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheckPos, colliderRadius);
        isGrounded = false;
        if (colliders.Length > 0)
        {
            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i] != mainCollider)
                {
                    isGrounded = true;
                    break;
                }
            }
        }

        // Apply movement velocity in the x direction
        r2d.velocity = new Vector2((moveDirection) * maxSpeed, r2d.velocity.y);

    }
}