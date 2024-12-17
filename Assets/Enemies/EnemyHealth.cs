using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
	public float MaxHealth = 100f;

	private float currentHealth;
	private Animator animator;
	private bool isDead;

	void Start()
	{
		currentHealth = MaxHealth;
		animator = GetComponent<Animator>();
	}

	public void TakeDamage(float damageAmount)
	{
		if (isDead)
		{
			return; // Avoid taking damage after death
		}

		currentHealth -= damageAmount;
		Debug.Log($"Enemy took {damageAmount} damage. Remaining health: {currentHealth}");

		if (animator != null)
		{
			animator.SetTrigger("TakeDamage");
		}

		if (currentHealth <= 0)
		{
			Die();
		}
	}

	private void Die()
	{
		if (isDead)
		{
			return; // Prevent multiple death triggers
		}

		isDead = true;

		if (animator != null)
		{
			animator.SetTrigger("Die");
		}

		GetComponent<AiChase>().enabled = false;

		// Destroy the enemy after the animation finishes
		Destroy(gameObject, 1f);
	}
}