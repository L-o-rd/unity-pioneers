using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class SpeedBoost : PowerupManager
{
    // Start is called before the first frame update

    [SerializeField]
    private float speedIncrease = 0.9f;
    public MockPlayerMovement mockPlayerMovement;
    public MockInGameTextUI mockInGameTextUI;
    public override void ActivatePowerUp(){
        if (testMode)
        {
            SoundManager.Instance.PlaySound(powerUpSound);
            mockPlayerMovement.SpeedUpPlayerBy(0.9f);
            mockInGameTextUI.ShowFeedback(itemDescription);
            DestroyImmediate(gameObject);
        }
        else
        {
            if (playerMovement != null)
            {
                SoundManager.Instance.PlaySound(powerUpSound);
                playerMovement.SpeedUpPlayerBy(speedIncrease);
                UnityEngine.Debug.Log("Speeding up Player");
                FindObjectOfType<InGameTextUI>().ShowFeedback(itemDescription);
                Destroy(gameObject);
            }
        }
            
    }

}
