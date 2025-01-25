using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashPowerup : PowerupManager
{
    public MockPlayerMovement mockPlayerMovement; // For testing
    public MockInGameTextUI mockInGameTextUI;     // For testing
    public override void ActivatePowerUp()
    {
        if (testMode)
        {
            Debug.Log("Test mode");
            SoundManager.Instance.PlaySound(powerUpSound);
            mockPlayerMovement?.ActivateDashPower();
            mockInGameTextUI?.ShowFeedback(itemDescription);
            DestroyImmediate(gameObject);
        }

        else
        {
            Debug.Log("Play mode");
            if (playerMovement!=null)
            {
                SoundManager.Instance.PlaySound(powerUpSound);
                playerMovement.ActivateDashPower();
                FindObjectOfType<InGameTextUI>().ShowFeedback(itemDescription);
                Destroy(gameObject);
            }
        }
    }
}
