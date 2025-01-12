using UnityEngine;

public class RicochetBullet : BaseBullet
{
    public int maxRicochets = 2; // Maximum number of ricochets allowed
    private int ricochetCount = 0;

    private Rigidbody2D rb2d; // Bullet's Rigidbody2D component
    private Vector2 lastFrameVelocity;

    protected override void Awake()
    {
        base.Awake();
        rb2d = GetComponent<Rigidbody2D>(); // Get the Rigidbody2D component
        ResetBulletState(); // Ensure the initial state is set only once
    }

    protected override void Update()
    {
        lastFrameVelocity = rb2d.velocity; // Store the velocity from the previous frame
        base.Update();
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        //if (collision.gameObject.CompareTag("Enemy"))
        //{
        //    // If the bullet hits an enemy, deactivate the bullet
        //    Debug.Log("Bullet hit an enemy. Returning to pool.");
        //    collision.gameObject.SetActive(false); // Optionally deactivate the enemy
        //    DeactivateBullet(); // Deactivate the bullet
        //    return; // Exit the function, no more ricochets
        //}

        if (ricochetCount < maxRicochets)
        {
            ricochetCount++;

            // Get the speed and direction of the last frame's velocity
            float speed = lastFrameVelocity.magnitude;
            Vector2 direction = Vector2.Reflect(lastFrameVelocity.normalized, collision.contacts[0].normal);

            // Set the new velocity (apply speed to the direction)
            rb2d.velocity = direction * Mathf.Max(speed, 0f); // Ensure speed doesn't go below 0

            // Optionally, update rotation to match new velocity direction
            float angle = Mathf.Atan2(rb2d.velocity.y, rb2d.velocity.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }
        else
        {
            // Deactivate the bullet after exceeding the ricochet limit
            DeactivateBullet(); // Deactivate the bullet
        }
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
    }

    // Reset ricochet count and velocity when the bullet is deactivated
    // Reset ricochet count and velocity when the bullet is deactivated
    public void ResetBullet(bool resetVelocity = true, bool resetRicochetCount = true)
{
    if (resetRicochetCount)
    {
        ricochetCount = 0; // Reset ricochet count
    }

    if (resetVelocity)
    {
        rb2d.velocity = Vector2.zero; // Reset velocity
    }

    transform.rotation = Quaternion.identity; // Reset rotation
    lastFrameVelocity = Vector2.zero; // Reset last frame velocity
}



    // Ensure the bullet's initial state is properly set
    public void ResetBulletState()
    {
        ricochetCount = 0; // Always start with a fresh ricochet count when first instantiated
    }
}
