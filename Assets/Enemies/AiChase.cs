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


	// Start is called before the first frame update
	void Start()
	{
		animator = GetComponent<Animator>();
	}

	// Update is called once per frame
	void Update()
	{
		distance = Vector2.Distance(transform.position, Player.transform.position);
		Vector2 direction = Player.transform.position - transform.position;

		direction.Normalize();

		if (distance < AttackRange)
		{
			isAttacking = true;
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

			transform.position =
				Vector2.MoveTowards(transform.position, Player.transform.position, Speed * Time.deltaTime);

			isChasing = true;
		}
		else
		{
			isChasing = false;
		}

		animator.SetBool("isChasing", isChasing);
		animator.SetBool("isAttacking", isAttacking);
	}
}
