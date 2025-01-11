using UnityEngine;

public class BulletHoming : MonoBehaviour
{
    public float homingSpeed = 10f;   // Speed of the homing bullet
    public float maxRange = 30f;      // Maximum range before the bullet disappears
    public float targetSearchRadius = 10f; // Radius to find nearby enemies

    private Vector3 startPosition;
    private Transform target;
    private Rigidbody2D rb;

    void OnEnable()
    {
        startPosition = transform.position;
        target = null; // Reset the target when the bullet is activated
        rb = GetComponent<Rigidbody2D>();  // Get the Rigidbody2D component
        rb.velocity = Vector2.zero;       // Reset velocity to prevent previous state interference
    }

    void Update()
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
        if (Vector3.Distance(startPosition, transform.position) > maxRange)
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

    private void OnTriggerEnter2D(Collider2D collision)
{
    // Check if the collision is with an enemy
    if (collision.CompareTag("Enemy"))
    {
        Debug.Log("Bullet hit an enemy");

        // Apply damage if the enemy has a health script attached
        var enemy = collision.GetComponent<EnemyHealth>();
        if (enemy != null)
        {
            Debug.Log("Enemy found, applying damage.");
            enemy.TakeDamage(20f); // Adjust the damage value as needed
        }

        // Deactivate the bullet after hitting an enemy
        DeactivateBullet();
    }
    // Check if the collision is with a room
    else if (collision.CompareTag("Room"))
    {
        Debug.Log("Bullet hit the room");

        // Deactivate the bullet after hitting the room
        DeactivateBullet();
    }
    else
    {
        // Handle any other collisions (optional, for debugging)
        Debug.Log("Bullet hit something else: " + collision.gameObject.name);
    }
}



    private void DeactivateBullet()
    {
        ObjectPool.instance.ReturnPooledObject(gameObject);
    }
}
