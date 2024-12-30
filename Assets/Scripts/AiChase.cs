using UnityEngine;

public class AiChase : MonoBehaviour
{
	public GameObject Player;
	public float Speed;
	public float DistanceBetween;
	public float AttackRange;

	private float distance;
	private bool isAttacking;
	private bool isChasing;
	private Animator animator;

	private const float ATTACK_COOLDOWN = 1f;
	private float lastAttackTime;

	private PlayerStats playerStats;

	void Start()
	{
		animator = GetComponent<Animator>();
		playerStats = Player.GetComponent<PlayerStats>();
	}

	void Update()
	{
		distance = Vector2.Distance(transform.position, Player.transform.position);
		Vector2 direction = Player.transform.position - transform.position;

		direction.Normalize();

		if (distance < AttackRange)
		{
			isAttacking = true;
			AttackPlayer();
		}
		else
		{
			isAttacking = false;
		}

		if (distance < DistanceBetween)
		{
			if (direction.x > 0)
			{
				transform.localScale = new Vector3(1, 1, 1);
			}
			else if (direction.x < 0)
			{
				transform.localScale = new Vector3(-1, 1, 1);
			}

			// Move towards the player
			transform.position = Vector2.MoveTowards(transform.position, Player.transform.position, Speed * Time.deltaTime);
			isChasing = true;
		}
		else
		{
			isChasing = false;
		}

		// Set animator states
		animator.SetBool("isChasing", isChasing);
		animator.SetBool("isAttacking", isAttacking);
	}

	void AttackPlayer()
	{
		if (Time.time > lastAttackTime + ATTACK_COOLDOWN)
		{
			if (playerStats != null)
			{
				playerStats.TakeDamage(10);
				Debug.Log("Player damaged: 10 health points.");
			}

			lastAttackTime = Time.time;
		}
	}
}
