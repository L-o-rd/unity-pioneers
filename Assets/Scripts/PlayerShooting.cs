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
        gunSpriteRenderer = gun.GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        HandleShooting();
        FlipGun();
    }

    private void HandleShooting()
    {
        if (Input.GetButton("Fire1") && Time.time >= nextFireTime)
        {
            nextFireTime = Time.time + (1f / equippedWeapon.fireRate);
            Shoot();
        }
    }

    private void Shoot()
    {
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

        // Get a bullet from the pool using the weapon's bullet type
        GameObject bullet = ObjectPool.instance.GetPooledObject(equippedWeapon.bulletType);
        if (bullet != null)
        {
            bullet.transform.position = firePoint.position;

            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0f;
            Vector2 direction = (mousePosition - firePoint.position).normalized;

            bullet.transform.up = direction;

            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.velocity = Vector2.zero;
                rb.AddForce(direction * equippedWeapon.bulletForce, ForceMode2D.Impulse);
            }
        }
        else
        {
            Debug.LogError($"No bullets available for type {equippedWeapon.bulletType}");
        }
    }

    private void FlipGun()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f;

        Vector2 direction = (mousePosition - gun.position).normalized;
        float angle = Vector2.SignedAngle(Vector2.right, direction);

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
        equippedWeapon = newWeapon;

        if (gunSpriteRenderer != null && equippedWeapon.gunSprite != null)
        {
            gunSpriteRenderer.sprite = equippedWeapon.gunSprite;
        }

        Debug.Log($"Equipped weapon: {newWeapon.weaponName}");
    }
}
