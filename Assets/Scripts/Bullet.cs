using UnityEngine;

public class Bullet : MonoBehaviour
{
	public float MaxRange = 30f; // Maximum range in units
	public float Damage = 20f; // Damage dealt by the bullet

	private Vector2 startPosition;

	void OnEnable()
	{
		startPosition = transform.position; // Save the bullet's starting position when activated
	}

	void Update()
	{
		// Check if the bullet has exceeded its maximum range
		float distanceTraveled = Vector2.Distance(startPosition, transform.position);
		if (distanceTraveled >= MaxRange)
		{
			gameObject.SetActive(false); // Deactivate the bullet after reaching max range
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		var enemy = collision.GetComponent<EnemyHealth>();
		Debug.Log("Bullet collided with " + collision.name);
		if (enemy != null)
		{
			enemy.TakeDamage(Damage);
			gameObject.SetActive(false);
		}
	}
}