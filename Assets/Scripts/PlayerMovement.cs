using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField]
    float moveSpeed = 2.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Get input from the horizontal and vertical axes (WASD or arrow keys)
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        // Create a new Vector3 to store the movement direction
        Vector2 movement = new Vector2(horizontalInput, verticalInput).normalized;
        // Normalize the direction vector to ensure that the player moves at a consistent speed
        // Move the player in the specified direction
        transform.Translate(movement * moveSpeed * Time.deltaTime);
    }
}
