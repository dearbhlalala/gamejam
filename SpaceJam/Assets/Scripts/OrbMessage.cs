using UnityEngine;
using TMPro;
using System.Collections; 

public class MessageTextBehavior : MonoBehaviour
{
    public TextMeshProUGUI messageText;

    public void HideMessageAfterDelay(float delay)
    {
        StartCoroutine(HideMessageCoroutine(delay));
    }

    private IEnumerator HideMessageCoroutine(float delay)
    {
        yield return new WaitForSeconds(delay);
        messageText?.gameObject.SetActive(false); 
    }
}
