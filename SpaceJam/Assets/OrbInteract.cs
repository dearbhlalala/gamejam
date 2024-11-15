using UnityEngine;

public class OrbInteract : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Trigger detected with: " + collision.name);
    }
}

