using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonCrate : Crate
{
    [SerializeField] private GameObject poisonEffectPrefab; 
    [SerializeField] private float poisonDamage = 10f;
    [SerializeField] private float poisonDuration = 5f;
    [SerializeField] private float poisonRadius = 2.5f;

    private bool hasPoisoned = false;

    public float getTotalPoisonDamage()
    {
        return poisonDamage * poisonDuration;
    }

    public void setTestPrefab(GameObject prefab){
        poisonEffectPrefab = prefab;
    }

    public void Poison()
    {
        if (poisonEffectPrefab != null)
        {
            GameObject poisonEffectObj = Instantiate(poisonEffectPrefab, transform.position, Quaternion.identity);

            SoundManager.Instance.PlaySound(poisonSound);
            var poisonEffect = poisonEffectObj.AddComponent<PoisonEffect>();
            poisonEffect.testMode = testMode;
            poisonEffect.Initialize(poisonDamage, poisonRadius, poisonDuration);
            Destroy(gameObject);
            Destroy(poisonEffect, poisonDuration);
        }

    }

    public override void InteractWithCrate()
    {
        if (hasPoisoned) return;
        Poison();
    }

    private void OnDrawGizmos()
    {
        // Visualize gas radius in the editor
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, poisonRadius);
    }
}
