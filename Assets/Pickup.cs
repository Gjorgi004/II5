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

        Debug.Log("interacted");

        if (inventory != null)
        {
            // Call your specific method
            if (inventory.AddItem(itemData))
            {
                Debug.Log($"Picked up {itemData.name}");
                Destroy(gameObject); // Remove from the world
            }
            else
            {
                Debug.Log("Inventory is full! Couldn't pick up.");
            }

        }
    }
}
