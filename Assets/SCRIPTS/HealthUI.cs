using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthUI : MonoBehaviour
{
    public TextMeshProUGUI HealthText;
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
        
        HealthText.text = Mathf.RoundToInt(currentHealth).ToString() + "%";
    }
}