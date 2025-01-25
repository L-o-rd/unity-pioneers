using System;
using System.Collections;
using System.Collections.Generic;
using Moq;
using UnityEngine;

public class Heart : PowerupManager
{
    private float heal=20f;
    public RoomManagerMock roomManagerMock;
    public MockPlayerStats mockPlayerStats;
    public MockInGameTextUI mockInGameTextUI;
    public void TestStart()
    {
        heal = heal/roomManagerMock.GetDifficulty();
    }
    void Start()
    {
        if (testMode)
        {
            TestStart();
            return;
        }
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
            Destroy(gameObject);
        }
            
    }

}
