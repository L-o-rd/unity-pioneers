using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;

    public Rigidbody2D rb;
    public Camera cam;
    
    public Transform gun; //Reference to the gun object
    public Transform body; // Reference to the body object


    Vector2 movement;
    Vector2 mousePos;

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        FlipGun();
    }
    void FixedUpdate() {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
        Vector2 lookDir = mousePos - rb.position;
        float angle = Mathf.Atan2(lookDir.y,lookDir.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = angle;
    }

    void FlipGun()
    {
        // Check if the gun's position is to the left or right of the body
        if (gun.position.x < body.position.x)
        {
            // Gun is to the left of the body, flip it
            if (gun.localScale.y > 0) // Ensure we only flip if it's not already flipped
            {
                gun.localScale = new Vector3(gun.localScale.x, -gun.localScale.y, gun.localScale.z);
            }
        }
        else
        {
            // Gun is to the right of the body, reset to original scale
            if (gun.localScale.y < 0) // Ensure we only flip back if it's currently flipped
            {
                gun.localScale = new Vector3(gun.localScale.x, -gun.localScale.y, gun.localScale.z);
            }
        }
    }
}
