using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    private readonly KeyCode right = KeyCode.D;
    private readonly KeyCode left = KeyCode.A;
    private readonly KeyCode down = KeyCode.S;
    private readonly KeyCode up = KeyCode.W;

    private float horizontal = 0f;
    private float vertical = 0f;
    private Vector2 movement;
    private Rigidbody2D rb;

    [SerializeField]
    private GameObject direction;

    [SerializeField]
    private float maxSpeed = 5.5f;

    private void Start() {
        rb = this.GetComponent<Rigidbody2D>();
    }

    private void Update() {
        horizontal = Input.GetKey(right) ? 1f : (Input.GetKey(left) ? -1f : 0f);
        vertical = Input.GetKey(up) ? 1f : (Input.GetKey(down) ? -1f : 0f);
        movement.x = horizontal;
        movement.y = vertical;
        movement.Normalize();
    }

    private void FixedUpdate() {
        rb.MovePosition(rb.position + movement * maxSpeed * Time.fixedDeltaTime);

        if (movement != Vector2.zero) {
            direction.SetActive(true);
            float angle = Mathf.Atan2(movement.y, movement.x) * Mathf.Rad2Deg;
            direction.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));
            direction.transform.position = rb.position + movement * 0.5f;
        } else direction.SetActive(false);
    }

    public void SlowPlayerBy(float percentage)
    {
        if (percentage < 0 || percentage > 1){
            Debug.LogWarning("Invalid percentage value");
            return;
        }
        maxSpeed *= percentage;
    }

    public void SpeedUpPlayerBy(float percentage)
    {
        if (percentage < 0 || percentage > 1){
            Debug.LogWarning("Invalid percentage value");
            return;
        }
        maxSpeed /= percentage;
    }
}