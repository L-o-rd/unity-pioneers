using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashPowerup : PowerupManager
{
    protected override void ActivatePowerUp()
    {
        if (playerMovement!=null)
        {
            SoundManager.Instance.PlaySound(powerUpSound);
            playerMovement.ActivateDashPower();
            FindObjectOfType<InGameTextUI>().ShowFeedback(itemDescription);
            Destroy(gameObject);
        }
    }

}
