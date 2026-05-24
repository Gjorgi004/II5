using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BossHealthbar : MonoBehaviour
{
    public float maxHealth = 100f;
    public float currentHealth;

    private bool isDead = false;
    public GameObject activedeathCamera;

    [Header("UI References")]
    public Image healthBarFill;
    public TextMeshProUGUI healthText;

    void Start()
    {
        currentHealth = maxHealth;
        UpdateUI();
    }

    // --- NEW: FORCES UI TO MATCH INSPECTOR CHANGES CONSTANTLY ---
    void Update()
    {
        UpdateUI();
    }
    // ------------------------------------------------------------

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        if (currentHealth <= 0)
        {
            float healthPercent = (currentHealth / maxHealth) * 100f;
        }
    }

    void UpdateUI()
    {
        if (healthBarFill != null)
            healthBarFill.fillAmount = currentHealth / maxHealth;

        if (healthText != null)
            healthText.text = currentHealth.ToString("F0") + "%";
    }
}