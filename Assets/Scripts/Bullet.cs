using UnityEngine;

public class Bullet : MonoBehaviour
{
	public float MaxRange = 30f; // Maximum range in units
	public float BaseDamage = 20f; // Damage dealt by the bullet
	private float Damage;
	[SerializeField]
	private PlayerStats playerStats;
	private Vector2 startPosition;

	public void SetDamage()
	{
		Damage = BaseDamage + playerStats.getBonusDamage();
	}
    void OnEnable()
	{
		startPosition = transform.position; // Save the bullet's starting position when activated
		SetDamage();
    }

    void Update()
	{
		// Debug.Log(Damage);
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
		// Debug.Log("Bullet collided with " + collision.name);
		if (enemy != null)
		{
			enemy.TakeDamage(Damage);
        }

        gameObject.SetActive(false);
    }
}