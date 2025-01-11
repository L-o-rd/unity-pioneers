using UnityEngine;

public class BaseBullet : MonoBehaviour
{
    public float maxRange = 30f; // Maximum range in units
    protected Vector2 startPosition;

    protected virtual void OnEnable()
    {
        startPosition = transform.position; // Save the bullet's starting position when activated
    }

    protected virtual void Update()
    {
        // Check if the bullet has exceeded its maximum range
        float distanceTraveled = Vector2.Distance(startPosition, transform.position);
        if (distanceTraveled >= maxRange)
        {
            gameObject.SetActive(false); // Deactivate the bullet after reaching max range
        }
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        gameObject.SetActive(false); // Default behavior: deactivate on collision
    }
}
