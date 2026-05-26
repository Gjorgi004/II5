using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;
using Kryz.CharacterStats.Examples;

public class EstusSystem : MonoBehaviour
{
    [Header("Input")]
    public KeyCode useHotkey = KeyCode.R;

    [Header("Flask Settings")]
    public Item estusItemAsset;         // Drag your generic Estus Flask ScriptableObject here
    public float healAmount = 150f;
    public int maxCharges = 5;
    public int currentCharges = 5;

    public EquipmentPanel equipmentpanel;
    public PlayerMovement playermovement;
    public ParticleSystem effect;
    public Light pointlight;
    public AudioSource healsfx;

    [Header("Equipment / Inventory Link")]
    // This represents whatever slot holds your currently active item.
    // If your system uses a different variable type for slots, change it here!
    

    [Header("HUD UI References")]
    public GameObject hudGroupPanel;
    public Image hudIconDisplay;
    public TextMeshProUGUI hudCountText;

    private PlayerHealth playerhealth;

    void Start()
    { 
        playerhealth = GetComponent<PlayerHealth>();
        currentCharges = maxCharges;
    }

  
    void Update()
    {
        bool isFlaskEquipped = CheckIfEquipped();
        UpdateHUD(isFlaskEquipped);

        // 2. Listen for input
        if (Input.GetKeyDown(useHotkey) && isFlaskEquipped)
        {
            Debug.Log($"Estus Handler: [R] pressed. Is flask equipped? {isFlaskEquipped}");

            if (isFlaskEquipped)
            {
                TryUseEstus();
            }
        }
    }

    bool CheckIfEquipped()
    {
        if (equipmentpanel == null || estusItemAsset == null) return false;

        // Pull the item directly out of your equipment slot array!
        // This mirrors exactly how your DebugFirstSlot() checks the Revolver
        Item equippedItem = equipmentpanel.GetItemAtSlot(0);

        return equippedItem == estusItemAsset;
    }

    void TryUseEstus()
    {
        if (playerhealth == null)
        {
            // Trying to re-grab the component just in case Start() missed it
            playerhealth = GetComponentInChildren<PlayerHealth>();

            if (playerhealth == null)
            {
                Debug.LogError("Estus Handler FAIL: Could not find the 'BossHealthbar' component on this Player GameObject!");
                return;
            }
        }

        if (currentCharges <= 0)
        {
            Debug.LogWarning("Estus Handler FAIL: 0 charges remaining.");
            return;
        }

        // --- LOG YOUR CURRENT HEALTH NUMBERS ---
        Debug.Log($"Estus Handler values: Current HP = {playerhealth.currentHealth} | Max HP = {playerhealth.maxHealth}");

        if (playerhealth.currentHealth >= playerhealth.maxHealth)
        {
            Debug.LogWarning("Estus Handler FAIL: Already at full health. Healing blocked.");
            return;
        }

        StartCoroutine(HealDelay());

        Debug.Log($"Estus Handler SUCCESS! New HP: {playerhealth.currentHealth}. Charges left: {currentCharges}");

    }

    private IEnumerator HealDelay()
    {
        playermovement.walkSpeed = 3f;
        healsfx.Play();
        pointlight.gameObject.SetActive(true);
        effect.Play();
        yield return new WaitForSeconds(1);
        pointlight.gameObject.SetActive(false);
        effect.Stop();
        currentCharges--;
        playerhealth.currentHealth += healAmount;
        playerhealth.currentHealth = Mathf.Clamp(playerhealth.currentHealth, 0f, playerhealth.maxHealth);
        playermovement.walkSpeed = 7f;

    }

    void UpdateHUD(bool isFlaskEquipped)
    {
        if (!isFlaskEquipped)
        {
            if (hudGroupPanel != null) hudGroupPanel.SetActive(false);
            return;
        }

        if (hudGroupPanel != null) hudGroupPanel.SetActive(true);

        if (hudCountText != null)
        {
            hudCountText.text = currentCharges.ToString();
        }
    }

    public void RefillFlask()
    {
        currentCharges = maxCharges;
    }

}
