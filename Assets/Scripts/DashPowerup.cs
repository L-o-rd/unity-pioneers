using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashPowerup : PowerupManager
{
    protected override void ActivatePowerUp()
    {
        if (playerMovement!=null)
        {
            playerMovement.ActivateDashPower();
            FindObjectOfType<InGameTextUI>().ShowFeedback(itemDescription);
            Destroy(gameObject);
        }
    }

}
