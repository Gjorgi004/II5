using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    public GameObject projectilePrefab; 
    public Transform player;            
    public float shootInterval = 2f;    
    public float bulletSpeed = 15f;

    void Start()
    {
        
        InvokeRepeating("Shoot", 2f, shootInterval);
    }

    void Shoot()
    {
        if (player == null) return;

        
        Vector3 spawnPos = transform.position + (transform.forward * 1.5f);

        
        GameObject bullet = Instantiate(projectilePrefab, spawnPos, Quaternion.identity);
        
        
        Vector3 direction = (player.position - spawnPos).normalized;
        
        
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        if(rb != null) 
        {
            rb.velocity = direction * bulletSpeed;
        }
    }
}
