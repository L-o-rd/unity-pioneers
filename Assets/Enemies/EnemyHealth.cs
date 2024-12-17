using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
	public float MaxHealth = 100f;
	private float currentHealth;

	void Start()
	{
		currentHealth = MaxHealth;
	}

	public void TakeDamage(float damageAmount)
	{
		currentHealth -= damageAmount;
		Debug.Log($"Enemy took {damageAmount} damage. Remaining health: {currentHealth}");

		if (currentHealth <= 0)
		{
			Die();
		}
	}

	private void Die()
	{
		Debug.Log("Enemy is dead!");
		// Add death logic here (play animations, drop loot, disable enemy, etc.)
		Destroy(gameObject);
	}
}