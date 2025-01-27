using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveCrate : Crate
{
    [SerializeField] private GameObject explosionEffectPrefab; // Reference to the particle prefab
    [SerializeField] private float explosionDamage = 30f;
    [SerializeField] private float explosionRange = 3f; // Area of effect for the explosion

    private bool hasExploded = false;

    public float getExplosionRange()
    {
        return explosionRange;
    }
    public float getExplosionDamage()
    {
        return explosionDamage;
    }
    public bool hasTrapExploded()
    {
        return hasExploded;
    }
    public void Explode()
    {
        if (hasExploded) return; // Prevent multiple explosions
        hasExploded = true;

        StartCoroutine(ExplosionDelay());

        // Play explosion sound
    }

    private IEnumerator ExplosionDelay()
    {
        Debug.Log("In enumerator");
        // Wait for 0.5 seconds
        yield return new WaitForSeconds(0.5f);

        // Create the explosion effect
        if (explosionEffectPrefab != null)
        {
            Instantiate(explosionEffectPrefab, transform.position, Quaternion.identity);
        }

        SoundManager.Instance.PlaySound(explosionSound);

        // Damage nearby objects
        Collider2D[] objectsInRange = Physics2D.OverlapCircleAll(transform.position, explosionRange);
        foreach (Collider2D obj in objectsInRange)
        {
            Debug.Log(obj);
            // Trigger explosions in other crates
            if (obj.CompareTag("Trap"))
            {
                Crate crate = obj.GetComponent<Crate>();
                if (crate != null)
                {
                    crate.InteractWithCrate();
                }
            }

            // Damage the player
            if (obj.CompareTag("Player"))
            {
                Debug.Log(testMode);
                if (testMode)
                {
                    MockPlayerStats mockPlayerStats = obj.GetComponent<MockPlayerStats>();
                    Debug.Log(mockPlayerStats);
                    if (mockPlayerStats != null)
                    {
                        mockPlayerStats.TakeDamage(explosionDamage);
                    }
                }
                
                PlayerStats playerStats = obj.GetComponent<PlayerStats>();
                if (playerStats != null && !playerStats.isTrapImmune())
                {
                    playerStats.TakeDamage(explosionDamage);
                }
            }

            if (obj.CompareTag("Enemy"))
            {
                if (testMode)
                {
                    MockEnemyHealth mockEnemyHealth = obj.GetComponent<MockEnemyHealth>();
                    if (mockEnemyHealth != null)
                    {
                        mockEnemyHealth.TakeDamage(explosionDamage);
                    }
                }

                EnemyHealth enemy = obj.GetComponent<EnemyHealth>();
                if (enemy != null)
                {
                    enemy.TakeDamage(explosionDamage);
                }
            }
        }
        // Destroy the crate
        Destroy(gameObject);
    }    
    public override void InteractWithCrate()
    {
        if (hasExploded) return;
            Explode();
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRange);
    }

    private void Start()
    {
        if (!testMode)
            explosionDamage = explosionDamage * GameObject.Find("RoomManager").GetComponent<RoomManager>().GetDifficulty();
    }

}
