using UnityEngine;

public class DestroyGreenOrb : MonoBehaviour
{
    public OrbDisplay orbDisplay;  

    void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.CompareTag("Player"))
        {
            orbDisplay.ShowMessage("Oh! Something opened!");  

            Destroy(gameObject); 
            Destroy(GameObject.Find("Door"));  
        }
    }
}
