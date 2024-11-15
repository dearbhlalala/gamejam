using UnityEngine;
using TMPro;

public class OrbAndDoorDeactivation : MonoBehaviour
{
    public GameObject door; 
    public TextMeshProUGUI messageText;
    public MessageTextBehavior messageBehavior;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            gameObject.SetActive(false);
            door?.SetActive(false); 
            
            if (messageText != null)
            {
                messageText.text = "Oh! Something opened!";
                messageText.gameObject.SetActive(true); 
                messageBehavior?.HideMessageAfterDelay(3f); 
            }
        }
    }
}



