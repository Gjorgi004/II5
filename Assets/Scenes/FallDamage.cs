using UnityEngine;

public class FallDamage : MonoBehaviour
{
    private CharacterController controller;
    public HealthUI healthSystem; 
    
    public float damageThreshold = -10f; 
    public float damageMultiplier = 5f;  

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
    
        if (controller.isGrounded)
        {
            float fallSpeed = controller.velocity.y;

            
            if (fallSpeed < damageThreshold)
            {
                
                float damage = Mathf.Abs(fallSpeed) * damageMultiplier;
                healthSystem.UpdateHealth(-damage); 
            }
        }
    }
}
