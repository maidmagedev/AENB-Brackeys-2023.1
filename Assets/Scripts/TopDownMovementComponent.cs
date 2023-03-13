using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class TopDownMovementComponent : MonoBehaviour
{
    Rigidbody2D rb;

    public Vector2 movementDirection;

    private float angle = 0f;
    [SerializeField] public float movementSpeed = 8f;

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
        // initializing the rigidbody
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
    }

    // Update is called once per frame
    void Update()
    {
        move();
        RotateSprite();
        resetDirection();
    }

    private void resetDirection(){
        movementDirection = Vector2.zero;
    }

    private void move()
    {
        Vector2 normalDir = movementDirection.normalized;
        // Directly sets the player's velocity based on input
        rb.velocity = new Vector2(normalDir.x * movementSpeed, normalDir.y * movementSpeed);
    }

    // Used by Animation Events.
    public void moveScripted(float dirX, float dirY)
    {
        // Directly sets the player's velocity based on input
        rb.velocity = new Vector2(dirX * movementSpeed, dirY * movementSpeed);
    }

    // Used by Animation Events.
    public void moveScripted(float dirX, float dirY, float ms)
    {
        // Directly sets the player's velocity based on input
        rb.velocity = new Vector2(dirX * ms, dirY * ms);
    }

    // Rotates the sprite based off mouse position--currently flips left or right only
    private void RotateSprite()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = mousePosition - transform.position;
        angle = Vector2.SignedAngle(Vector2.right, direction);
        //print("angle: " + angle);
        
        // uncomment this to rotate sprite based off mouse position
        //transform.eulerAngles = new Vector3(0, 0, angle);

        if (Mathf.Abs(angle) > 100)
        {
            // facing left
            gameObject.transform.localScale = new Vector2(-1f, gameObject.transform.localScale.y);
            //print("facing left");
        }
        else if (Mathf.Abs(angle) > - 100) // was < 40
        {
            // facing right
            gameObject.transform.localScale = new Vector2(1f, gameObject.transform.localScale.y);
            //print("facing right");
        }

    }

    // Flips the sprite on the y axis based off the player's movement direction
    private void FlipSprite()  
    {
        if (rb.velocity.x < 0) // made this velocity based instead
        {
            gameObject.transform.localScale = new Vector2(-0.3f, gameObject.transform.localScale.y);
        }
        else if (rb.velocity.x > 0)
        {
            gameObject.transform.localScale = new Vector2(0.3f, gameObject.transform.localScale.y);
        }

    }
}
