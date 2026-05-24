using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Kryz.CharacterStats.Examples;

public class BossTarget : MonoBehaviour
{

    public Item itemData;
    public Inventory inventory;

    public string bossName = "Critter of the forest";

    public ParticleSystem blood;
    public ParticleSystem bloodkill;
    public PlayerSouls playersouls;
    private bool isDead = false;

    public int souls = 0;

    public Image healthBarFill; 
    public TextMeshProUGUI nameText;

    public AudioSource audiosource;
    public GameObject healthbar;

    public float maxHealth = 1000f;
    public float health = 1000f;

    private void Start()
    {
        health = maxHealth;
        InitializeBossUI();
    }

    private void Update()
    {
        UpdateHealthUI();
    }

    void UpdateHealthUI()
    {
        if (healthBarFill != null)
        {
            
            healthBarFill.fillAmount = health / maxHealth;
        }
    }
    void InitializeBossUI()
    {
        if (nameText != null)
            nameText.text = bossName;

        UpdateHealthUI();
    }

    public void TakeDamage(float amount)
    {
        if (isDead) return;

        blood.Play();
        audiosource.Play();

        health -= amount;
        health = Mathf.Clamp(health, 0, maxHealth);

        if (health <= 0f)
        {
            Die();
        }
    }
    public void ToggleUIFrame(bool SetActiveState)
    {
        if (healthBarFill != null)
        {
            // Finds the parent Canvas panel holding the bar and names, and toggles it
            healthbar.SetActive(SetActiveState);
            nameText.gameObject.SetActive(SetActiveState);
        }
    }
    void Die ()
        {
        isDead = true;

        AudioSource killsfx = bloodkill.GetComponent<AudioSource>();
        Instantiate(bloodkill, transform.position + (Vector3.up * 1.1f), Quaternion.identity, null);

        killsfx.Play();
        bloodkill.Play();
        playersouls.AddSouls(souls);



        if (inventory == null)
         {
            inventory = GameObject.FindObjectOfType<Inventory>();
           }

          if (inventory != null)
          {
             if (inventory.AddItem(itemData))
             {
                 Debug.Log("Key gotten!");
             }
         }
        if (healthBarFill != null)
        {
            // Shuts off the parent panel holding the health bar and names
            healthBarFill.transform.parent.gameObject.SetActive(false);
            nameText.gameObject.SetActive(false);
        }

        Destroy(gameObject);
        }

    }
