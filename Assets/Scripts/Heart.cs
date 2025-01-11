using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : PowerupManager
{
    private float heal=20f;
    void Start()
    {
        heal /= GameObject.Find("RoomManager").GetComponent<RoomManager>().GetDifficulty();
    }
    protected override void ActivatePowerUp(){
        playerStats.Heal(heal);
        FindObjectOfType<InGameTextUI>().ShowWorldFeedback("+"+heal+"HP");
        Debug.Log("Player was healed for "+heal+" health");
        Destroy(gameObject);
    }

}
