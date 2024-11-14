using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
   public float moveSpeed = 5f;
    public float jumpForce = 5f;
    
    private Rigidbody2D rb;
    private bool isJumping = false;
    
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    
    private void Update()
    {
        float moveX = Input.GetAxis("Horizontal");
        
        // Move the player horizontally
        rb.velocity = new Vector2(moveX * moveSpeed, rb.velocity.y);
        
        // Jumping
        if (Input.GetButtonDown("Jump") && !isJumping)
        {
            rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
            isJumping = true;
        }
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the player is touching the ground
        if (collision.gameObject.CompareTag("Ground"))
        {
            isJumping = false;
        }
    }
}
