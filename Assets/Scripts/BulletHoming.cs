using UnityEngine;

public class BulletHoming : BaseBullet
{
    public float homingSpeed = 10f;   // Speed of the homing bullet
    public float targetSearchRadius = 10f; // Radius to find nearby enemies

    private Transform target;
    private Rigidbody2D rb;

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        target = null; // Reset the target when the bullet is activated
        rb = GetComponent<Rigidbody2D>();  // Get the Rigidbody2D component
        rb.velocity = Vector2.zero;       // Reset velocity to prevent previous state interference
        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous; // Ensure accurate collision detection
    }

    protected override void Update()
    {
        FindNearestTarget();

        if (target != null)
        {
            // Calculate direction towards the target
            Vector2 direction = (target.position - transform.position).normalized;
            // Apply velocity in the direction of the target
            rb.velocity = direction * homingSpeed;
        }
        else
        {
            // If no target, just move in a straight line
            rb.velocity = transform.up * homingSpeed;
        }

        // If the bullet exceeds its max range, deactivate it
        if (Vector3.Distance(startPosition, transform.position) > MaxRange)
        {
            DeactivateBullet();
        }
    }

    void FindNearestTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        float closestDistance = targetSearchRadius;
        GameObject nearestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null)
        {
            target = nearestEnemy.transform;
        }
        else
        {
            target = null;
        }
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
    }
    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision); 
    }
}
