using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public float damageAmount = 15f; // How much damage the attack does

    // This fires automatically whenever the object hits something with a collider
    private void OnTriggerEnter(Collider other)
    {
        // Check if the object we bumped into is the Player
        if (other.CompareTag("Player"))
        {
            // Try to find the PlayerHealth script on that object
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();

            if (playerHealth != null)
            {
                // Deal the damage!
                playerHealth.TakeDamage(damageAmount);
                Debug.Log("Enemy hit the player for " + damageAmount + " damage!");
            }

            // Destroy the debug sphere immediately on impact so it doesn't double-hit
            Destroy(gameObject);
        }
    }
}