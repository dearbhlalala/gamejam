using UnityEngine;

public class DMovement : MonoBehaviour
{
    public float moveSpeed = 5f;  
    public float jumpForce = 10f;   

    private Rigidbody2D rb;         

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); 
    }

    void Update()
    {
        
        float moveInput = Input.GetAxisRaw("Horizontal"); 

        
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

    
        if (Input.GetButtonDown("Jump")) 
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }
}
