using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireRatePowerup : PowerupManager
{
    // Start is called before the first frame update

    [SerializeField]
    private float fireRateBonus = 1.15f;

    public MockInGameTextUI mockInGameTextUI;
    public MockPlayerShooting mockPlayerShooting;
    public override void ActivatePowerUp(){
        if (testMode)
        {
            SoundManager.Instance.PlaySound(powerUpSound);
            float currentFireRate = mockPlayerShooting.fireRateMultiplier;
            mockPlayerShooting.setFireRateMultiplier(Math.Min(currentFireRate*fireRateBonus, 2.0f));
            FindObjectOfType<MockInGameTextUI>().ShowFeedback(itemDescription);
            DestroyImmediate(gameObject);
        }
        else
        {
            if (playerShooting != null)
            {
                SoundManager.Instance.PlaySound(powerUpSound);
                float currentFireRate = playerShooting.getFireRateMultiplier();
                playerShooting.setFireRateMultiplier(Math.Min(currentFireRate*fireRateBonus, 2.0f));
                FindObjectOfType<InGameTextUI>().ShowFeedback(itemDescription);
                Destroy(gameObject);
            }
        }
            
    }

}
