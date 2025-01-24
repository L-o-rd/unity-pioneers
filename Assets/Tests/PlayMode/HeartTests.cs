using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;

public class HeartTests : MonoBehaviour
{
    private MockPlayerStats playerStatsMock;
    private Heart heart;
    private MockSoundManager soundManagerMock;
    private MockInGameTextUI inGameTextUI;
    private RoomManagerMock roomManagerMock;

    [SetUp]
    public void SetUp()
    {

        var player = new GameObject();
        playerStatsMock = player.AddComponent<MockPlayerStats>();
        playerStatsMock.MaxHealth = 100f;

        var uiObject = new GameObject();
        inGameTextUI = uiObject.AddComponent<MockInGameTextUI>();

        var roomManagerMockObj = new GameObject();
        roomManagerMock = roomManagerMockObj.AddComponent<RoomManagerMock>();
        roomManagerMock.SetDifficulty(1.0f);

        var heartObject = new GameObject();
        heart = heartObject.AddComponent<Heart>();
        heart.testMode=true;
        heart.mockPlayerStats=playerStatsMock;
        heart.mockInGameTextUI = inGameTextUI;
        heart.roomManagerMock = roomManagerMock;
        heart.Initialize();

        soundManagerMock = new MockSoundManager();
        SoundManager.Instance = soundManagerMock;
    }

    [TearDown]
    public void TearDown()
    {
        Object.DestroyImmediate(playerStatsMock.gameObject);
        Object.DestroyImmediate(roomManagerMock.gameObject);
        Object.DestroyImmediate(inGameTextUI.gameObject);
    }

    [Test]
    public void ActivatePowerup_HealPlayer()
    {
        heart.ActivatePowerUp();

        Assert.AreEqual(120f, playerStatsMock.MaxHealth);

        Assert.IsTrue(soundManagerMock.SoundPlayed);
        
        Assert.AreEqual("+20HP",inGameTextUI.LastFeedback);
    }
}
