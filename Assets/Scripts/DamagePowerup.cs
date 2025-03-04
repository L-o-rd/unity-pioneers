using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePowerup : PowerupManager
{
    [SerializeField] private int damageBoost = 3;

    protected override void ActivatePowerUp()
    {
        if (playerStats != null)
        {
            SoundManager.Instance.PlaySound(powerUpSound);
            playerStats.setPlayerDamage(playerStats.getPlayerDamage() + damageBoost);
            FindObjectOfType<InGameTextUI>().ShowFeedback(itemDescription);
            Destroy(gameObject);
        }
    }

}
