using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class SpeedBoost : PowerupManager
{
    // Start is called before the first frame update

    [SerializeField]
    private float speedIncrease = 0.9f;
    protected override void ActivatePowerUp(){
        if (playerMovement != null){
            playerMovement.SpeedUpPlayerBy(speedIncrease);
            UnityEngine.Debug.Log("Speeding up Player");
            FindObjectOfType<InGameTextUI>().ShowFeedback(itemDescription);
            gameObject.SetActive(false);
        }
    }

}
