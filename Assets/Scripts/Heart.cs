using System;
using System.Collections;
using System.Collections.Generic;
using Moq;
using UnityEngine;

public class Heart : PowerupManager
{
    private float heal=20f;
    public bool testMode = false;
    public RoomManagerMock roomManagerMock;
    public MockPlayerStats mockPlayerStats;
    public MockInGameTextUI mockInGameTextUI;
    public void Initialize()
    {
        heal = heal/roomManagerMock.GetDifficulty();
    }
    void Start()
    {
        heal =Mathf.Floor(heal/GameObject.Find("RoomManager").GetComponent<RoomManager>().GetDifficulty());
    }
    public override void ActivatePowerUp(){
        if (testMode)
        {
            SoundManager.Instance.PlaySound(healthSound);
            mockPlayerStats.heal(heal);
            mockInGameTextUI.ShowFeedback("+"+heal+"HP");
        }
        else
        {
            SoundManager.Instance.PlaySound(healthSound);
            playerStats.Heal(heal);
            FindObjectOfType<InGameTextUI>().ShowWorldFeedback("+"+heal+"HP",Color.red);
            Debug.Log("Player was healed for "+heal+" health");
            Destroy(gameObject);
        }
            
    }

}
