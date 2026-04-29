using UnityEngine;
using Kryz.CharacterStats.Examples;

public class Target : MonoBehaviour
{

    public Item itemData;
    public Inventory inventory;


    public float health = 50f;

    public void TakeDamage(float amount)
    {

        health -= amount;
        if (health <= 0f)
        {
            Die();
        }
    }
        void Die ()
        {
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
                Debug.Log("Key gotten!");
            }
        }

        Destroy(gameObject);
        }

    }
