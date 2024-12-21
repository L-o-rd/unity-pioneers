using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonCrate : MonoBehaviour
{
    [SerializeField] private GameObject poisonEffectPrefab; 
    [SerializeField] private float poisonDamage = 10f;
    [SerializeField] private float poisonDuration = 5f;
    [SerializeField] private float poisonRadius = 2.5f;


    private void Poison()
    {
        if (poisonEffectPrefab != null)
        {
            GameObject poisonEffect = Instantiate(poisonEffectPrefab, transform.position, Quaternion.identity);
            poisonEffect.AddComponent<PoisonEffect>().Initialize(poisonDamage, poisonRadius, poisonDuration);
            Destroy(poisonEffect, poisonDuration);
        }

    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            Poison();
            Destroy(gameObject);
        }
    }

    private void OnDrawGizmos()
    {
        // Visualize gas radius in the editor
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, poisonRadius);
    }
}
