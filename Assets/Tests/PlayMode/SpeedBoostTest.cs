using NUnit.Framework;
using UnityEngine;
using System.Collections;

public class SpeedBoostTests : MonoBehaviour
{
    private GameObject playerObject;
    private MockPlayerMovement playerMovementMock;
    private SpeedBoost powerup;
    private MockInGameTextUI inGameTextUI;

    [SetUp]
    public void SetUp()
    {
        // Create player object
        playerObject = new GameObject("Player");
        playerMovementMock = playerObject.AddComponent<MockPlayerMovement>();

        // Create powerup object
        var powerupObject = new GameObject("Powerup");
        powerup = powerupObject.AddComponent<SpeedBoost>();

        // Create mock InGameTextUI
        var uiObject = new GameObject("UI");
        inGameTextUI = uiObject.AddComponent<MockInGameTextUI>();

        // Mock SoundManager (optional if used elsewhere)
        SoundManager.Instance = new MockSoundManager();

        powerup.testMode = true;
        powerup.mockInGameTextUI=inGameTextUI;
        powerup.mockPlayerMovement=playerMovementMock;
    }

    [TearDown]
    public void TearDown()
    {
        Object.DestroyImmediate(playerObject);
        Object.DestroyImmediate(inGameTextUI.gameObject);
    }

    [Test]
    public void ActivatePowerup_IncreasesSpeed()
    {
        // Arrange
        powerup.itemDescription = "Speed Boost Activated";
        playerMovementMock.maxSpeed=5f;

        // Act
        powerup.ActivatePowerUp();

        // Assert
        Assert.IsTrue(playerMovementMock.speedIncreased);
        Assert.AreEqual("Speed Boost Activated", inGameTextUI.LastFeedback);
        Assert.IsTrue(Mathf.Round(playerMovementMock.maxSpeed * 10f) / 10f - 5.5<=0.1f);
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
