using UnityEngine;

public class AiChase : MonoBehaviour
{
	public float Speed;
	public float DistanceBetween;
	public float AttackRange;

	private GameObject player;
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

		// Attempt to find the player in the scene by tag
		player = GameObject.FindGameObjectWithTag("Player");

		if (player != null)
		{
			playerStats = player.GetComponent<PlayerStats>();
		}
		else
		{
			Debug.LogWarning("Player not found! Make sure the Player has the 'Player' tag.");
		}
	}

	void Update()
	{
		if (player == null) return;

		distance = Vector2.Distance(transform.position, player.transform.position);
		Vector2 direction = player.transform.position - transform.position;

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
			var scale = transform.localScale;
			if (direction.x > 0)
			{
				transform.localScale = new Vector3(Mathf.Abs(scale.x), scale.y, scale.z);
			}
			else if (direction.x < 0)
			{
				transform.localScale = new Vector3(-Mathf.Abs(scale.x), scale.y, scale.z);
			}

			// Move towards the player
			transform.position = Vector2.MoveTowards(transform.position, player.transform.position, Speed * Time.deltaTime);
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
