using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePowerup : PowerupManager
{
    [SerializeField] private float damageBoost = 5f;

    protected override void ActivatePowerUp()
    {
        if (bulletManager != null)
        {
            bulletManager.AddGlobalDamageBoost(damageBoost);
            FindObjectOfType<InGameTextUI>().ShowFeedback(itemDescription);
            Destroy(gameObject);
        }
        else
        {
            Debug.LogError("Bullet prefab not assigned to DamagePowerup script!");
        }
    }

}
