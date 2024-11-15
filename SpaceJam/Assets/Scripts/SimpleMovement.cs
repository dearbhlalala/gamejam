using UnityEngine;

public class SimpleMovement: MonoBehaviour
{
    public float moveSpeed = 5f;   // Movement speed of the object
    public float jumpForce = 10f;   // Jump force

    private Rigidbody2D rb;         // Reference to the Rigidbody2D component

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Get the Rigidbody2D component
    }

    void Update()
    {
        // Get horizontal input (left-right movement)
        float moveInput = Input.GetAxisRaw("Horizontal"); // -1 (left) to 1 (right)

        // Apply horizontal movement (using Rigidbody2D)
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

        // Handle jumping (spacebar or "Jump" button)
        if (Input.GetButtonDown("Jump")) // Jump on spacebar or "Jump" button
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce); // Set vertical velocity for jump
        }
    }
}
