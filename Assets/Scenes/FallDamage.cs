using UnityEngine;

public class FallDamage : MonoBehaviour
{
    private Rigidbody rb; 
    public HealthUI healthSystem;
    
    public float damageThreshold = -8f; 
    public float damageMultiplier = 5f;

    void Start()
    {
        
        rb = GetComponent<Rigidbody>();
    }

    
    private void OnCollisionEnter(Collision collision)
    {
        
        float fallSpeed = rb.velocity.y;

        if (fallSpeed < damageThreshold)
        {
            float damage = Mathf.Abs(fallSpeed) * damageMultiplier;
            healthSystem.UpdateHealth(-damage);
            Debug.Log("Fall Damage Taken: " + damage);
        }
    }
}