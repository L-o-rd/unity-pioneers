using UnityEngine;
using NUnit.Framework;
using System.Collections;

public class FireRatePowerupTests
{
    private GameObject player;
    private MockPlayerShooting playerShootingMock;
    private FireRatePowerup powerup;
    private MockSoundManager mockSoundManager;
    private MockInGameTextUI inGameTextUI;

    [SetUp]
    public void SetUp()
    {
        player = new GameObject();
        playerShootingMock = player.AddComponent<MockPlayerShooting>();
        playerShootingMock.setFireRateMultiplier(1.0f);

        var fireRatePowerupObject = new GameObject();
        powerup = fireRatePowerupObject.AddComponent<FireRatePowerup>();

        var uiObject = new GameObject();
        inGameTextUI = uiObject.AddComponent<MockInGameTextUI>();

        mockSoundManager = new MockSoundManager();
        SoundManager.Instance = mockSoundManager;
        powerup.mockInGameTextUI=inGameTextUI;
        powerup.mockPlayerShooting=playerShootingMock;
        powerup.testMode = true;

    }

    [TearDown]
    public void TearDown()
    {
        Object.DestroyImmediate(player);
        Object.DestroyImmediate(inGameTextUI.gameObject);
    }

    [Test]
    public void ActivatePowerUp_AllMethodsCalled()
    {
        powerup.itemDescription = "Fire Rate Powerup Active";
        powerup.ActivatePowerUp();

        Assert.AreEqual(1.15f, playerShootingMock.fireRateMultiplier);

        Assert.IsTrue(mockSoundManager.SoundPlayed);
        
        Assert.AreEqual("Fire Rate Powerup Active",inGameTextUI.LastFeedback);

    }

    [Test]
    public void PowerupDestroyed()
    {
        powerup.ActivatePowerUp();

        Assert.IsTrue(powerup == null || powerup.Equals(null));
    }

    [Test]
    public void ActivatePowerup_MaxFireRateCapReached()
    {
        playerShootingMock.setFireRateMultiplier(1.99f);
        powerup.ActivatePowerUp();

        Assert.AreEqual(2.0f, playerShootingMock.fireRateMultiplier);
    }
}
