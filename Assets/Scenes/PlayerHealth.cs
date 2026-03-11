using UnityEngine;
using UnityEngine.UI;
using TMPro; 

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    public float currentHealth;

    [Header("UI References")]
    public Image healthBarFill; 
    
    
    public TextMeshProUGUI healthText; 

    void Start()
    {
        currentHealth = maxHealth;
        UpdateUI();
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        UpdateUI();

        if (currentHealth <= 0) 
        {
            Debug.Log("Player Dead!");
        }
    }

    void UpdateUI()
    {
        if (healthBarFill != null) healthBarFill.fillAmount = currentHealth / maxHealth;
        
        
        if (healthText != null) healthText.text = currentHealth.ToString("F0") + "%";
    }
}
