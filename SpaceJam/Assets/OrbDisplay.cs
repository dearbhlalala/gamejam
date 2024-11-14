using UnityEngine;
using TMPro;

public class OrbDisplay : MonoBehaviour
{
    public TextMeshProUGUI messageText;  
    private float clearTextTimer = 0f;   

    void Update()
    {
        if (clearTextTimer > 0f)
        {
            clearTextTimer -= Time.deltaTime; 

            if (clearTextTimer <= 0f)
            {
                messageText.text = "";  
            }
        }
    }

    public void ShowMessage(string message)
    {
        messageText.text = message;  
        clearTextTimer = 2f;  
    }
}
