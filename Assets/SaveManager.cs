using UnityEngine;
using AASave; // Uses the AA Save and Load System namespace [cite: 79]
using Kryz.CharacterStats.Examples; // Matches your project namespace

public class SaveManager : MonoBehaviour
{
    [Header("Save System")]
    public SaveSystem saveSystem; // Drag your 'Save System' GameObject here [cite: 79]

    [Header("Panel References")]
    public Inventory playerInventory;
    public EquipmentPanel equipmentPanel;

    public void SaveGameProgress()
    {
        if (saveSystem == null) return;

        // --- 1. SAVE INVENTORY ITEMS ---
        if (playerInventory != null)
        {
            // Call your new helper function to get the names array cleanly
            string[] invNames = playerInventory.GetItemNamesForSaving();
            saveSystem.Save("InventoryData", invNames); // [cite: 79, 166]
        }

        // --- 2. SAVE EQUIPMENT SLOTS ---
        if (equipmentPanel != null)
        {
            EquipmentSlot[] eqSlots = equipmentPanel.GetEquipmentSlots();
            string[] eqNames = new string[eqSlots.Length];
            for (int i = 0; i < eqSlots.Length; i++)
            {
                eqNames[i] = eqSlots[i].Item != null ? eqSlots[i].Item.name : "";
            }
            saveSystem.Save("EquipmentData", eqNames); // [cite: 79, 166]
        }

        // --- 3. SAVE GLOBAL STATUS FIELDS ---
        //saveSystem.Save("BossDefeated", BossTarget.isBossDead); // [cite: 79]

      //  saveSystem.Save(); // Commit writes to disk
        Debug.Log("Game data successfully saved!");
    }

    public void LoadGameProgress()
    {
        if (saveSystem == null) return;

       
        if (playerInventory != null && saveSystem.DoesDataExists("InventoryData"))
        {
            string[] loadedInv = saveSystem.LoadArray("InventoryData").AsStringArray();

            // Call your new clear function safely
            playerInventory.ClearInventoryForLoading();

            foreach (string name in loadedInv)
            {
                if (string.IsNullOrEmpty(name)) continue;
                Item itemAsset = playerInventory.FindItemByName(name);
                if (itemAsset != null)
                {
                    playerInventory.AddItem(itemAsset); // Uses your real existing AddItem method
                }
            }
        }

        // --- 2. LOAD EQUIPMENT ---
        if (equipmentPanel != null && saveSystem.DoesDataExists("EquipmentData")) // [cite: 257, 258]
        {
            string[] loadedEq = saveSystem.LoadArray("EquipmentData").AsStringArray(); // [cite: 188, 245]
            EquipmentSlot[] eqSlots = equipmentPanel.GetEquipmentSlots();
            equipmentPanel.ClearAllEquipmentSlots();

            for (int i = 0; i < loadedEq.Length && i < eqSlots.Length; i++)
            {
                if (string.IsNullOrEmpty(loadedEq[i])) continue;

                EquippableItem eqAsset = equipmentPanel.FindEquippableByName(loadedEq[i]);
                if (eqAsset != null)
                {
                    eqSlots[i].Item = eqAsset;
                }
            }
        }

        // --- 3. LOAD CORE STATUS PROGRESSION ---
       // BossTarget.isBossDead = saveSystem.Load("BossDefeated", false).AsBool(); // [cite: 188]

        Debug.Log("Game data successfully loaded!");
    }

    void Update()
    {
        // Debug wipe key for testing
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            if (saveSystem.DoesDataExists("InventoryData")) saveSystem.Delete("InventoryData"); // [cite: 252, 254, 257]
            if (saveSystem.DoesDataExists("EquipmentData")) saveSystem.Delete("EquipmentData"); // [cite: 252, 254, 257]
            if (saveSystem.DoesDataExists("BossDefeated")) saveSystem.Delete("BossDefeated"); // [cite: 252, 254, 257]

            if (equipmentPanel != null) equipmentPanel.ClearAllEquipmentSlots();
            if (playerInventory != null) playerInventory.ClearInventoryForLoading();
           // BossTarget.isBossDead = false;

            Debug.Log("Save files deleted!");
        }
    }
}