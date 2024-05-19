using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairMovement : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    float horizontalInput;
    float verticalInput;
    float moveLimiter = 0.7f;
    [SerializeField] public float movementSpeed = 8f;
    [SerializeField] bool MovementDisabled = true;
    [SerializeField] bool goingDown = true;
    [SerializeField] TopDownMovementComponent playerMovement;
    [SerializeField] bool rightIsDown;
    
    // Start is called before the first frame update
    void Start()
    {
        //rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!MovementDisabled)
        {
            playerMovement.DisableMovement();
        } else
        {
            playerMovement.EnableMovement();
            return;
        }
        // Getting the Player's input
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
        Debug.Log(horizontalInput);
        if (horizontalInput != 0)
        {
            if (verticalInput == 0)
            {
                if (goingDown)
                {
                    verticalInput = -1;
                }
                else
                {
                    verticalInput = 1;
                }
            }
        } 
        
            

        

        if (horizontalInput != 0 && verticalInput != 0)
        {
            // limit movement speed diagonally, so you move at 70% speed
            horizontalInput *= moveLimiter;
            verticalInput *= moveLimiter;
        }
        move();
        
    }

    

    public void DisableMovement()
    {
        //rb.velocity = Vector2.zero;
        MovementDisabled = true;
    }

    public void EnableMovement()
    {
        // rb.velocity = Vector2.zero;
        MovementDisabled = false;
    }

    private void move()
    {
        // Directly sets the player's velocity based on input
        rb.velocity = new Vector2(horizontalInput * movementSpeed, verticalInput * movementSpeed);
    }

}

