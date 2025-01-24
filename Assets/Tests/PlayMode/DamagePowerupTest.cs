using NUnit.Framework;
using UnityEngine;

public class DamagePowerupTest
{
    private GameObject player;
    private MockPlayerStats playerStatsMock;
    private DamagePowerup powerup;
    private MockSoundManager mockSoundManager;
    private MockInGameTextUI inGameTextUI;

    [SetUp]
    public void SetUp()
    {
        player = new GameObject();
        playerStatsMock = player.AddComponent<MockPlayerStats>();

        var damagePowerupObject = new GameObject();
        powerup = damagePowerupObject.AddComponent<DamagePowerup>();

        var uiObject = new GameObject();
        inGameTextUI = uiObject.AddComponent<MockInGameTextUI>();

        mockSoundManager = new MockSoundManager();
        SoundManager.Instance = mockSoundManager;
        powerup.mockInGameTextUI=inGameTextUI;
        powerup.mockPlayerStats=playerStatsMock;
        powerup.testMode = true;

    }

    [TearDown]
    public void TearDown()
    {
        Object.DestroyImmediate(player);
        Object.DestroyImmediate(inGameTextUI.gameObject);
    }

    [Test]
    public void ActivatePowerUp_IncreaseDamage_ThenDestroysItself()
    {
        powerup.mockPlayerStats.setPlayerDamage(20);
        powerup.itemDescription = "Damage Powerup Active";
        powerup.ActivatePowerUp();

        Assert.AreEqual(23, powerup.mockPlayerStats.Damage);

        // Check if the sound was played
        Assert.IsTrue(mockSoundManager.SoundPlayed);

        Assert.AreEqual("Damage Powerup Active",powerup.mockInGameTextUI.LastFeedback);

        // Check if the powerup object was destroyed
        Assert.IsTrue(powerup == null || powerup.Equals(null));
    }
}