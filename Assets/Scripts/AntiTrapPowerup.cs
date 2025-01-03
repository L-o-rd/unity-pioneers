using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntiTrapPowerup : PowerupManager
{
    protected override void ActivatePowerUp(){
        if (playerStats!=null){
            playerStats.setTrapImmune(true);
            UnityEngine.Debug.Log("Speeding up Player");
            FindObjectOfType<InGameTextUI>().ShowFeedback(itemDescription);
            Destroy(gameObject);
        }
    }
}
