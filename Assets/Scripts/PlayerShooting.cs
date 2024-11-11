using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public Transform firePoint;
    public float bulletForce = 20f;

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        // Get an inactive bullet from the pool
        GameObject bullet = ObjectPool.instance.GetPooledObject();
        if (bullet != null)
        {
            // Set the bullet's position and rotation to the fire point
            bullet.transform.position = firePoint.position;
            bullet.transform.rotation = firePoint.rotation;
            bullet.SetActive(true); // Activate the bullet

            // Reset velocity for the Rigidbody2D to ensure consistent movement
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.velocity = Vector2.zero; // Reset velocity
            rb.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse); // Apply force to shoot
        }
    }
}
