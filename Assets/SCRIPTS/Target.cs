using UnityEngine;
using Kryz.CharacterStats.Examples;

public class Target : MonoBehaviour
{

    public Item itemData;
    public Inventory inventory;

    public ParticleSystem blood;
    public ParticleSystem bloodkill;
    

    public float health = 50f;

    public void TakeDamage(float amount)
    {
        blood.Play();
        health -= amount;
        if (health <= 0f)
        {
            Instantiate(bloodkill, transform.position + (Vector3.up * 1.1f), Quaternion.identity, null);
            bloodkill.Play();
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
