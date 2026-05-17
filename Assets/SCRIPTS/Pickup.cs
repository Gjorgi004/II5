using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kryz.CharacterStats.Examples;

public class Pickup : MonoBehaviour, IInteractable
{

    public EquippableItem itemData;
    public Inventory inventory;

    public void Interact()
    {
        // If you haven't manually dragged the inventory in the inspector...
        if (inventory == null)
        {
            // Unity scans all objects for that "Inventory" script
            inventory = GameObject.FindObjectOfType<Inventory>();
        }

        if (inventory != null)
        {
            // Try adding the item
            if (inventory.AddItem(itemData))
            {
                Destroy(gameObject);
            }
        }
    }
}
