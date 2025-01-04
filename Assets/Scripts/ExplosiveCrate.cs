using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveCrate : MonoBehaviour
{
    [SerializeField] private GameObject explosionEffectPrefab; // Reference to the particle prefab
    [SerializeField] private float explosionDamage = 30f;
    [SerializeField] private float explosionRange = 3f; // Area of effect for the explosion

    private void Explode()
    {
        if (explosionEffectPrefab != null)
        {
            Instantiate(explosionEffectPrefab, transform.position, Quaternion.identity);
        }

        Collider2D[] objectsInRange = Physics2D.OverlapCircleAll(transform.position, explosionRange);
        foreach (Collider2D obj in objectsInRange)
        {
            if (obj.CompareTag("Player"))
            {
                PlayerStats playerStats = obj.GetComponent<PlayerStats>();
                if (playerStats != null && !playerStats.isTrapImmune())
                {
                    playerStats.TakeDamage(explosionDamage);
                }
            }
            // Damage enemies script here: 
        }

        // Destroy the crate
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            Explode(); 
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRange);
    }

    private void Start()
    {
        explosionDamage = explosionDamage * GameObject.Find("RoomManager").GetComponent<RoomManager>().GetDifficulty();
    }

}
