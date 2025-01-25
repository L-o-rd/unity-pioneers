using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePowerup : PowerupManager
{
    [SerializeField] private int damageBoost = 3;
    public MockPlayerStats mockPlayerStats; // For testing
    public MockInGameTextUI mockInGameTextUI; // For testing
    public override void ActivatePowerUp()
    {
        if (testMode)
        {
            SoundManager.Instance.PlaySound(powerUpSound);
            mockPlayerStats.setPlayerDamage(mockPlayerStats.Damage + damageBoost);
            FindObjectOfType<MockInGameTextUI>().ShowFeedback(itemDescription);
            DestroyImmediate(gameObject);
        }

        else
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

}
