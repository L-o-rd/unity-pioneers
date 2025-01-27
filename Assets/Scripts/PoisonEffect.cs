using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonEffect : MonoBehaviour
{
    private float poisonDamage;
    private float poisonRadius;
    private float poisonDuration;

    public bool testMode = false;
    public void Initialize(float damage, float radius, float duration)
    {
        if (testMode)
            poisonDamage = damage;
        else
            poisonDamage = damage*GameObject.Find("RoomManager").GetComponent<RoomManager>().GetDifficulty();
        poisonRadius = radius;
        poisonDuration = duration;

        StartCoroutine(ApplyPoisonDamage());
        Destroy(gameObject, poisonDuration+0.01f); // Destroy the poison effect after duration
    }

    private IEnumerator ApplyPoisonDamage()
    {
        float elapsedTime = 0f;

        while (elapsedTime < poisonDuration)
        {
            Collider2D[] objectsInRange = Physics2D.OverlapCircleAll(transform.position, poisonRadius);
            foreach (Collider2D obj in objectsInRange)
            {
                if (obj.CompareTag("Player"))
                {
                    if (testMode)
                    {
                        MockPlayerStats mockPlayerStats = obj.GetComponent<MockPlayerStats>();
                        if (mockPlayerStats != null)
                        {
                            mockPlayerStats.TakeDamage(poisonDamage);
                        }
                    }
                    PlayerStats playerStats = obj.GetComponent<PlayerStats>();
                    if (playerStats != null && !playerStats.isTrapImmune())
                    {
                        playerStats.TakeDamage(poisonDamage);
                    }
                }
                if (obj.CompareTag("Enemy"))
                {
                    if (testMode)
                    {
                        MockEnemyHealth mockEnemyHealth = obj.GetComponent<MockEnemyHealth>();
                        if (mockEnemyHealth != null)
                        {
                            mockEnemyHealth.TakeDamage(poisonDamage);
                        }
                    }
                    EnemyHealth enemy = obj.GetComponent<EnemyHealth>();
                    if (enemy != null)
                    {
                        enemy.TakeDamage(poisonDamage);
                    }
                }
            }

            elapsedTime += 1f;
            yield return new WaitForSeconds(1f);
        }
    }
}
