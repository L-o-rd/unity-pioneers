using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float maxRange = 30f; // Maximum range in units
    private Vector2 startPosition;

    void OnEnable()
    {
        startPosition = transform.position; // Save the bullet's starting position when activated
    }

    void Update()
    {
        // Check if the bullet has exceeded its maximum range
        float distanceTraveled = Vector2.Distance(startPosition, transform.position);
        if (distanceTraveled >= maxRange)
        {
            gameObject.SetActive(false); // Deactivate the bullet after reaching max range
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Deactivate the bullet upon collision without creating an effect
        gameObject.SetActive(false);
    }
}