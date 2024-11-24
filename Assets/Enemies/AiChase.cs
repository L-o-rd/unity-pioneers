using UnityEngine;

public class AiChase : MonoBehaviour
{
	public GameObject player;
	public float speed;
	public float distanceBetween;

	private float distance;

	// Start is called before the first frame update
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		distance = Vector2.Distance(transform.position, player.transform.position);
		Vector2 direction = player.transform.position - transform.position;

		direction.Normalize();

		var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;


		if (distance < distanceBetween)
		{
			transform.position =
				Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);

			transform.rotation = Quaternion.Euler(Vector3.forward * angle);
		}
	}
}
