using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    public float damage = 10f; 

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Enemy")) 
        {
            return; 
        }

        
        if (other.CompareTag("Player")) 
        {
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
            }

            
            Destroy(gameObject);
        }
        else 
        {
            
            Destroy(gameObject);
        }
    }
}
