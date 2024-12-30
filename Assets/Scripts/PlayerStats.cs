using UnityEngine;

public class PlayerStats : MonoBehaviour
{
	private bool isDead = false;
	private float health;

	[SerializeField]
	private float maxHealth = 100;

	private void Start()
	{
		health = maxHealth;
	}

	private void Update()
	{
		if (isDead)
		{
			return;
		}

		// Debug: reduce health with the Space key
		if (Input.GetKey(KeyCode.Space))
		{
			TakeDamage(1);
		}

		if (health <= 0)
		{
			isDead = true;
			Die();
		}
	}

	public void TakeDamage(float amount)
	{
		if (isDead)
		{
			return;
		}

		health -= amount;
		Debug.Log($"Health remaining: {health}");

		if (health <= 0)
		{
			isDead = true;
			Die();
		}
	}

	private void Die()
	{
		Debug.Log("Player is dead!");
	}
}
