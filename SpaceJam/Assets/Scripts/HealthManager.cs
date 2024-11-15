using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{

    public Image healthBar;
    public float healthAmount = 100f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
        if (healthAmount <= 0)
        {
            Scene currentScene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(currentScene.name);
        }
        
        
        
        
        if (Input.GetKeyDown(KeyCode.X) || Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.K))
        {
            TakeDamage(10f);
        }
    }

    public void TakeDamage(float damage)
    {
        healthAmount = damage;
        healthBar.fillAmount = healthAmount / 100f;
    }

    public void Heal(float healingAmount)
    {
        healthAmount += healingAmount;
        healthAmount = Mathf.Clamp(healthAmount, 0, 100);

        healthBar.fillAmount = healthAmount / 100f;
    }



 
}
