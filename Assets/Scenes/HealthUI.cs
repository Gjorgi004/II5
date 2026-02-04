using UnityEngine;
using UnityEngine.UI; 

public class HealthUI : MonoBehaviour
{
    public Image healthBarFill;
    public float maxHealth = 100f;
    private float currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void UpdateHealth(float amount)
    {
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        
        
        healthBarFill.fillAmount = currentHealth / maxHealth;
    }
}
