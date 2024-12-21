using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonEffect : MonoBehaviour
{
    private float poisonDamage;
    private float poisonRadius;
    private float poisonDuration;

    public void Initialize(float damage, float radius, float duration)
    {
        poisonDamage = damage*GameObject.Find("RoomManager").GetComponent<RoomManager>().GetBonusDifficulty();
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
                    PlayerStats playerStats = obj.GetComponent<PlayerStats>();
                    if (playerStats != null)
                    {
                        playerStats.TakeDamage(poisonDamage);
                    }
                }
            }

            elapsedTime += 1f;
            yield return new WaitForSeconds(1f);
        }
    }
}
