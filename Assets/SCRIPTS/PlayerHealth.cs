using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    public float currentHealth;
    public GameObject camera;
    public GameObject deathCamera;
    public Transform respawnPoint;

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
        if (isDead && Input.GetKeyDown(KeyCode.R))
        {
            transform.parent.position = respawnPoint.position;
            transform.parent.rotation = respawnPoint.rotation;
            camera.SetActive(true);
            Destroy(activedeathCamera);
            isDead = false;
            currentHealth = maxHealth;
        }
    }
    // ------------------------------------------------------------

    public void TakeDamage(float amount)
    {

        if (isDead) return;

        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        if (currentHealth <= 0)
        {
            isDead = true;
            camera.SetActive(false);
            activedeathCamera = Instantiate(deathCamera, transform.position, transform.rotation);
            Debug.Log("Player Dead!");


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