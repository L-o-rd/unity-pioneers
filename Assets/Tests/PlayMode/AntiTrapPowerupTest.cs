using NUnit.Framework;
using UnityEngine;

public class AntiTrapPowerupTests
{
    private GameObject playerObject;
    private MockPlayerStats playerStatsMock;
    private AntiTrapPowerup powerup;
    private MockInGameTextUI inGameTextUI;
    private MockSoundManager mockSoundManager;

    [SetUp]
    public void SetUp()
    {
        // Create player object
        playerObject = new GameObject("Player");
        playerStatsMock = playerObject.AddComponent<MockPlayerStats>();

        // Create powerup object
        var powerupObject = new GameObject("Powerup");
        powerup = powerupObject.AddComponent<AntiTrapPowerup>();

        // Create mock InGameTextUI
        var uiObject = new GameObject("UI");
        inGameTextUI = uiObject.AddComponent<MockInGameTextUI>();

        // Mock SoundManager (optional if used elsewhere)
        
        mockSoundManager = new MockSoundManager();
        SoundManager.Instance = mockSoundManager;
        powerup.testMode = true;
        powerup.mockInGameTextUI = inGameTextUI;
        powerup. mockPlayerStats = playerStatsMock;
    }

    [TearDown]
    public void TearDown()
    {
        Object.DestroyImmediate(playerObject);
        Object.DestroyImmediate(inGameTextUI.gameObject);
    }

    [Test]
    public void ActivatePowerup_SetsTrapImmune()
    {
        // Arrange
        powerup.itemDescription = "Anti-Trap Powerup Activated";

        // Act
        powerup.ActivatePowerUp();

        // Assert
        Assert.IsTrue(playerStatsMock.TrapImmune);
        Assert.IsTrue(mockSoundManager.SoundPlayed);
        Assert.AreEqual("Anti-Trap Powerup Activated", inGameTextUI.LastFeedback);
    }

    [Test]
    public void ActivatePowerup_DestroysPowerupObject()
    {
        // Act
        powerup.ActivatePowerUp();

        // Assert
        Assert.IsTrue(powerup == null || powerup.Equals(null)); // Verify destruction
    }
}