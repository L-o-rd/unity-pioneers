using UnityEngine;
using System.Collections.Generic;

public class BaseBullet : MonoBehaviour
{
    private PlayerStats playerStats;
    public float MaxRange = 30f; // Maximum range in units
    public float BaseDamage = 20f; // Damage dealt by the bullet
    protected Vector2 startPosition;
    public float Damage;
    private static readonly HashSet<string> excludedTags = new HashSet<string> { "Reward", "Player", "Trap" };

    [SerializeField]
    protected AudioClip hitSound;

    protected virtual void Awake()
    {
        playerStats = FindObjectOfType<PlayerStats>();
    }

    private void SetDamage()
    {
        Damage = BaseDamage + playerStats.getPlayerDamage() + playerStats.getBonusDamage();
    }

    protected virtual void OnEnable()
    {
        startPosition = transform.position; // Save the bullet's starting position when activated
        SetDamage();
    }

    protected virtual void Update()
    {
        // Check if the bullet has exceeded its maximum range
        float distanceTraveled = Vector2.Distance(startPosition, transform.position);
        if (distanceTraveled >= MaxRange)
        {
            DeactivateBullet();
        }
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag!="Player"){
            SoundManager.Instance.PlaySound(hitSound);
            DeactivateBullet();
        }
        // Default behavior: deactivate on collision
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
	{
        var enemy = collision.GetComponent<EnemyHealth>();
        // Debug.Log("Bullet collided with " + collision.name);
        if (enemy != null)
        {
            var rageMeter = FindObjectOfType<RageMeter>();
            if (rageMeter != null)
            {
                rageMeter.AddRage(Mathf.Floor(Damage / 10));
            }

            enemy.TakeDamage(Damage);
            SoundManager.Instance.PlaySound(hitSound);
            DeactivateBullet();
        }

        var crate = collision.GetComponent<Crate>();
        if (crate != null)
        {
            crate.InteractWithCrate();
            SoundManager.Instance.PlaySound(hitSound);
            DeactivateBullet();
        }

        
    }

    public void DeactivateBullet()
    {
        ObjectPool.instance.ReturnPooledObject(gameObject);
    }
}
