using System.Collections;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [Header("Shooting Settings")]
    public Transform firePoint;         // Where the bullets are fired from
    public Transform gun;               // The gun object for flipping and aiming
    public Transform body;              // Player's body for flipping checks
    public Weapon equippedWeapon;       // The currently equipped weapon (ScriptableObject)
    
    private SpriteRenderer gunSpriteRenderer; // To reference the gun's sprite renderer
    private bool isFlipped = false;     // Tracks if the gun is flipped
    private float nextFireTime = 0f;    // Time until the next shot is allowed

    void Start()
    {
        // Get the SpriteRenderer component of the gun to change its sprite
        gunSpriteRenderer = gun.GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        HandleShooting();
        FlipGun();
    }

    private void HandleShooting()
    {
        // Check if the fire button is pressed and if we can fire
        if (Input.GetButton("Fire1") && Time.time >= nextFireTime)
        {
            nextFireTime = Time.time + (1f / equippedWeapon.fireRate);
            Shoot();
        }
    }

    private void Shoot()
    {
        // Validate weapon and object pool
        if (equippedWeapon == null)
        {
            Debug.LogError("No weapon equipped!");
            return;
        }

        if (ObjectPool.instance == null)
        {
            Debug.LogError("ObjectPool instance is missing!");
            return;
        }

        // Get a bullet from the pool
        GameObject bullet = ObjectPool.instance.GetPooledObject();
        if (bullet != null)
        {
            // Set bullet's position and rotation
            bullet.transform.position = firePoint.position;

            // Calculate direction to the mouse position
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0f;
            Vector2 direction = (mousePosition - firePoint.position).normalized;

            // Rotate the bullet to face the mouse
            bullet.transform.up = direction;

            // Activate the bullet
            bullet.SetActive(true);

            // Apply force to the bullet
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.velocity = Vector2.zero; // Reset velocity
                rb.AddForce(direction * equippedWeapon.bulletForce, ForceMode2D.Impulse);
            }
            else
            {
                Debug.LogError("Bullet is missing a Rigidbody2D component!");
            }
        }
        else
        {
            Debug.LogError("No pooled objects available in ObjectPool!");
        }
    }

    private void FlipGun()
    {
        // Get the mouse position in world coordinates
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f;

        // Calculate direction from gun to mouse
        Vector2 direction = (mousePosition - gun.position).normalized;

        // Determine angle between gun's right direction and mouse direction
        float angle = Vector2.SignedAngle(Vector2.right, direction);

        // Flip the gun sprite if necessary
        if (angle > 90f || angle < -90f)
        {
            if (!isFlipped)
            {
                gun.localScale = new Vector3(gun.localScale.x, -Mathf.Abs(gun.localScale.y), gun.localScale.z);
                isFlipped = true;
            }
        }
        else
        {
            if (isFlipped)
            {
                gun.localScale = new Vector3(gun.localScale.x, Mathf.Abs(gun.localScale.y), gun.localScale.z);
                isFlipped = false;
            }
        }
    }

    public void EquipWeapon(Weapon newWeapon)
    {
        // Equip a new weapon by assigning it to the equippedWeapon
        equippedWeapon = newWeapon;

        // Change the gun sprite to match the new weapon's gun sprite
        if (gunSpriteRenderer != null && equippedWeapon.gunSprite != null)
        {
            gunSpriteRenderer.sprite = equippedWeapon.gunSprite;
        }

        Debug.Log($"Equipped weapon: {newWeapon.weaponName}");
    }
}
