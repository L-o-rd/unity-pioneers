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

		var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

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
			transform.position =
				Vector2.MoveTowards(transform.position, Player.transform.position, Speed * Time.deltaTime);

			transform.rotation = Quaternion.Euler(Vector3.forward * angle);

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
