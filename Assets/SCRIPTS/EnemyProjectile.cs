using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{

    public float speed = 10f;
    public int damage = 25;
    public float lifetime = 5f;

    public float trackingStrength = 4f;
    private Transform playerTransform;
    public AudioSource whooshsound;

    private Vector3 moveDirection;

    private void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player")?.transform;
        whooshsound.Play();
    }

    public void SetupDirection(Vector3 targetDirection)
    {
        moveDirection = targetDirection.normalized;
        Destroy(gameObject, lifetime); // Self-destruct timer
    }

    private void Update()
    {
        Vector3 targetDir = ((playerTransform.position + Vector3.up * 1f) - transform.position).normalized;
        moveDirection = Vector3.Lerp(moveDirection, targetDir, trackingStrength * Time.deltaTime).normalized;

        transform.position += moveDirection * speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerMovement movement = other.GetComponentInParent<PlayerMovement>();
        // Check if it hit the player
        if (other.CompareTag("Player"))
        {
            if (movement.dashing == true) return;

            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>(); // Try to find the PlayerHealth script (adjust the component name to match yours)
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
            }

            Destroy(gameObject);
        }
        // Destroy it if it hits solid walls/floors, but ignore other enemies
        else if (other.gameObject.layer != LayerMask.NameToLayer("Enemy"))
        {
           // Destroy(gameObject);
        }
    }


}
