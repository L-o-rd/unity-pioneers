using UnityEngine;

public class BulletHoming : MonoBehaviour
{
    public float homingSpeed = 10f;   // Speed of the homing bullet
    public float maxRange = 30f;      // Maximum range before the bullet disappears
    public float targetSearchRadius = 10f; // Radius to find nearby enemies

    private Vector3 startPosition;    // Starting position to calculate the range
    private Transform target;         // The current target

    void Start()
    {
        // Save the bullet's starting position
        startPosition = transform.position;
    }

    void Update()
    {
        // Continuously search for the nearest target
        FindNearestTarget();

        // Move toward the target if it exists
        if (target != null)
        {
            Vector2 direction = (target.position - transform.position).normalized;
            transform.position += (Vector3)direction * homingSpeed * Time.deltaTime;
        }

        // Destroy the bullet if it exceeds the maximum range
        if (Vector3.Distance(startPosition, transform.position) > maxRange)
        {
            Destroy(gameObject);
        }
    }

    void FindNearestTarget()
    {
        // Find all GameObjects with the tag "Enemy"
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        float closestDistance = targetSearchRadius; // Start with the maximum search radius
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

        // Assign the closest enemy as the target if found
        if (nearestEnemy != null)
        {
            target = nearestEnemy.transform;
        }
        else
        {
            target = null; // No enemy within range
        }
    }

    // Optional: Handle collision with the target
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform == target)
        {
            Debug.Log("Target hit!");
            Destroy(collision.gameObject); // Destroy the target
            Destroy(gameObject); // Destroy the bullet
        }
    }
}
