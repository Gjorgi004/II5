using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class BossAI : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;
    public LayerMask whatIsGround, whatIsPlayer;
    public Animator animator;
    public float health;

    // Patroling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;
    private bool isCurrentlyMoving;

    // Attacking
    public float timeBetweenAttacks;
    bool alreadyAttacked;

    public enum BossState {ShootOrbs, Groundslam}
    public BossState currentAttackState = BossState.ShootOrbs;

    public GameObject projectile;
    public Transform throwPoint;
    public float attackCooldown = 3f;
    private bool canAttack = true;

    // States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    // State safety switches
    private bool isAttacking = false;
    private string currentAnimationState = "Idle";

    public float Windup = 1f;

    public PlayerMovement playerMovement;

    public AudioSource zombiesound;
    public AudioSource zombiesound2;
    public AudioSource groundslam;
    public ParticleSystem flash;
    public ParticleSystem Glow;
    public ParticleSystem Slam;

    private void Awake()
    {
        player = GameObject.Find("PlayerObj").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (isAttacking) return; // Completely freeze checks during attack/cooldown sequence

        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (!playerInSightRange && !playerInAttackRange) Patroling();
        if (playerInSightRange && !playerInAttackRange) ChasePlayer();
        if (playerInAttackRange && playerInSightRange) AttackPlayer();
    }

    private void Patroling()
    {
        agent.isStopped = false;
        ChangeAnimationState("Run"); // Or "Walk" if you use it for patrolling

        if (!walkPointSet) SearchWalkPoint();
        if (walkPointSet) agent.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;
        if (distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;
    }

    private void SearchWalkPoint()
    {
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);
        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
            walkPointSet = true;
    }

    private void ChasePlayer()
    {
        agent.isStopped = false;
        agent.SetDestination(player.position);

        if (!isCurrentlyMoving)
        {
            animator.ResetTrigger("Reached"); // Clear old attack data
            animator.SetTrigger("Chase");     // Fires your Run state
            zombiesound.Play();
            isCurrentlyMoving = true;
        }
    }

    private void AttackPlayer()
    {
        isAttacking = true;
        agent.isStopped = true;
        agent.velocity = Vector3.zero;

        isCurrentlyMoving = false;

        Vector3 targetPosition = new Vector3(player.position.x, transform.position.y, player.position.z);
        transform.LookAt(targetPosition);

        if (!alreadyAttacked)
        {
            switch (currentAttackState)
            {

                case BossState.ShootOrbs:
                   StartCoroutine(Attackwindup1());
                break;

                case BossState.Groundslam:
                    StartCoroutine(BlinkRoutine());
                    break;

            }
            
        }
    }

    private IEnumerator Attackwindup1()
    {

        alreadyAttacked = true;
        zombiesound2.Play();
        animator.SetTrigger("Reached");
        Glow.gameObject.SetActive(true);

        yield return new WaitForSeconds(Windup);
        Glow.gameObject.SetActive(false);

        for (int i = 0; i < 5; i++)
        {
            GameObject projectedSphere = Instantiate(projectile, throwPoint.position, Quaternion.identity);
            projectedSphere.transform.position = throwPoint.position;

            // Aim towards the player's torso/chest level
            Vector3 targetTorsoPos = player.position + Vector3.up * 1f;
            Vector3 throwDirection = targetTorsoPos - throwPoint.position;

            // Find the script attached to the sphere prefab and pass it the travel direction
            EnemyProjectile dynamicScript = projectedSphere.GetComponent<EnemyProjectile>();
            if (dynamicScript != null)
            {
                dynamicScript.SetupDirection(throwDirection);
            }
            yield return new WaitForSeconds(0.1f);
        }

        yield return new WaitForSeconds(timeBetweenAttacks);
        currentAttackState = BossState.Groundslam;
        ResetAttack();
    }

    private IEnumerator BlinkRoutine()
    {
        animator.SetTrigger("SecondAttack");
        flash.Play();
        yield return new WaitForSeconds(1f);
        Slam.Play();
        groundslam.Play();

        Vector3 blinkTarget = player.position - (player.forward * 2f);
        blinkTarget.y = transform.position.y; // Stay grounded

        transform.position = blinkTarget;
        transform.LookAt(new Vector3(player.position.x, transform.position.y, player.position.z));

        Vector3 enemyFlatPos = new Vector3(transform.position.x, 0, transform.position.z);
        Vector3 playerFlatPos = new Vector3(player.position.x, 0, player.position.z);
        float distanceToPlayer = Vector3.Distance(enemyFlatPos, playerFlatPos);
        float strikeBuffer = 1.2f;

        float maxAttackRange = attackRange + strikeBuffer;

        if (distanceToPlayer <= maxAttackRange)
        {
            if (playerMovement != null && playerMovement.dashing)
            {
                Debug.Log("Dodged!");
                goto SkipDamage;
            }

            PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(15f); // Deals 15 damage instantly
                Debug.Log("Direct Melee Hit! Player health is now: " + playerHealth.currentHealth);
            }

            Vector3 spawnPosition = transform.position + transform.forward * 1.2f + transform.up * 1f;
            // GameObject debugHitbox = Instantiate(projectile, spawnPosition, Quaternion.identity);
            // Destroy(debugHitbox, 0.2f);


        }
    SkipDamage:
        yield return new WaitForSeconds(timeBetweenAttacks);
        currentAttackState = BossState.ShootOrbs;
        ResetAttack();

    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
        isAttacking = false;
        agent.isStopped = false;
        ChangeAnimationState("Idle"); // Default back to idle to rest
    }

    // A state machine system that prevents spamming triggers frame by frame
    private void ChangeAnimationState(string newAnimationState)
    {
        if (currentAnimationState == newAnimationState) return;

        // Reset all triggers to prevent animations from stacking up weirdly
        animator.ResetTrigger("Chase");
        animator.ResetTrigger("Reached");

        if (newAnimationState == "Run") animator.SetTrigger("Chase");
        if (newAnimationState == "Attack") animator.SetTrigger("Reached");
        if (newAnimationState == "Idle") animator.SetTrigger("Chase"); // Or whatever drops it back down

        currentAnimationState = newAnimationState;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
}