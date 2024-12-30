using UnityEngine;

public class RicochetBullet : BaseBullet
{
    public int maxRicochets = 3; // Maximum number of ricochets allowed
    private int ricochetCount = 0;

    private Rigidbody2D rb2d; // Bullet's Rigidbody2D component
    private Vector2 lastFrameVelocity;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>(); // Get the Rigidbody2D component
    }

    void Update()
    {
        lastFrameVelocity = rb2d.velocity; // Store the velocity from the previous frame
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // Dacă glonțul lovește un "Enemy", dezactivează glonțul
            Debug.Log("Bullet hit an enemy. Returning to pool.");
            collision.gameObject.SetActive(false); // Dezactivăm inamicul lovit (opțional)
            gameObject.SetActive(false); // Dezactivează glonțul
            return; // Ieșim din funcție, nu mai ricochetează
        }

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
            gameObject.SetActive(false); // Dezactivează glonțul
        }
    }

    // Reset ricochet count and velocity when the bullet is deactivated
    public void ResetBullet()
    {
        ricochetCount = 0;
        rb2d.velocity = Vector2.zero; // Reset velocity to prevent strange behavior
        transform.rotation = Quaternion.identity; // Reset rotation
    }
}
