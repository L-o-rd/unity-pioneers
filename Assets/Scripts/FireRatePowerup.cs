using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireRatePowerup : PowerupManager
{
    // Start is called before the first frame update

    [SerializeField]
    private float fireRateBonus = 1.15f;
    protected override void ActivatePowerUp(){
        if (playerShooting != null){
            float currentFireRate = playerShooting.getFireRateMultiplier();
            playerShooting.setFireRateMultiplier(Math.Max(currentFireRate*fireRateBonus, 2f));
            FindObjectOfType<InGameTextUI>().ShowFeedback(itemDescription);
            Destroy(gameObject);
        }
    }

}
