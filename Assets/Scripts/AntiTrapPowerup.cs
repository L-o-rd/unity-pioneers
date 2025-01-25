using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntiTrapPowerup : PowerupManager
{
    public MockPlayerStats mockPlayerStats;
    public MockInGameTextUI mockInGameTextUI;
    public override void ActivatePowerUp(){
        if (testMode)
        {
            SoundManager.Instance.PlaySound(powerUpSound);
            mockPlayerStats.setTrapImmune(true);
            mockInGameTextUI.ShowFeedback(itemDescription);
            DestroyImmediate(gameObject);
        }
        else
        {
            if (playerStats!=null)
            {
                SoundManager.Instance.PlaySound(powerUpSound);
                playerStats.setTrapImmune(true);
                UnityEngine.Debug.Log("Speeding up Player");
                FindObjectOfType<InGameTextUI>().ShowFeedback(itemDescription);
                Destroy(gameObject);
            }
        }
 
    }

}
