using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : PowerupManager
{
    private float heal=20f;
    void Start()
    {
        heal =Mathf.Floor(heal/GameObject.Find("RoomManager").GetComponent<RoomManager>().GetDifficulty());
    }
    protected override void ActivatePowerUp(){
        SoundManager.Instance.PlaySound(healthSound);
        playerStats.Heal(heal);
        FindObjectOfType<InGameTextUI>().ShowWorldFeedback("+"+heal+"HP",Color.red);
        Debug.Log("Player was healed for "+heal+" health");
        Destroy(gameObject);
    }

}
