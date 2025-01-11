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
    private void OnTriggerEnter2D(Collider2D collision)
	{
		// var enemy = collision.GetComponent<EnemyHealth>();
		// // Debug.Log("Bullet collided with " + collision.name);
		// if (enemy != null)
		// {
		// 	var rageMeter = FindObjectOfType<RageMeter>();
		// 	if (rageMeter != null)
		// 	{
		// 		rageMeter.AddRage(Mathf.Floor(Damage / 10));
		// 	}
		// 	enemy.TakeDamage(Damage);
		// 	gameObject.SetActive(false);
		// 	return;
		// }
		var crate = collision.GetComponent<Crate>();
		if (crate != null)
		{
			crate.InteractWithCrate();
			gameObject.SetActive(false);
		}

	}
}
