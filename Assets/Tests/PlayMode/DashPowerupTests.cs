using NUnit.Framework;
using UnityEngine;
public class DashPowerupTest
{
    private GameObject player;
    private MockPlayerMovement playerMovementMock;
    private DashPowerup dashPowerup;
    private MockSoundManager soundManagerMock;
    private MockInGameTextUI inGameTextUI;

    [SetUp]
    public void SetUp()
    {
        // Player setup
        player = new GameObject();
        playerMovementMock = player.AddComponent<MockPlayerMovement>();

        // Powerup setup
        var dashPowerupObject = new GameObject();
        dashPowerup = dashPowerupObject.AddComponent<DashPowerup>();
        dashPowerup.mockPlayerMovement = playerMovementMock;

        // Sound manager setup
        soundManagerMock = new MockSoundManager();
        SoundManager.Instance = soundManagerMock;

        // UI mock setup
        var uiObject = new GameObject();
        inGameTextUI = uiObject.AddComponent<MockInGameTextUI>();
        dashPowerup.mockInGameTextUI = inGameTextUI;
        
        dashPowerup.testMode=true;
    }

    [TearDown]
    public void TearDown()
    {
        Object.DestroyImmediate(player);
        Object.DestroyImmediate(inGameTextUI.gameObject);
    }

    [Test]
    public void ActivatePowerUp_PlayerMovementNotNull_ActivatesDashPower()
    {
        dashPowerup.itemDescription="Dash Powerup Active";
        dashPowerup.ActivatePowerUp();
        Assert.IsTrue(playerMovementMock.dashActive);
        Assert.IsTrue(soundManagerMock.SoundPlayed);
        Assert.AreEqual("Dash Powerup Active", inGameTextUI.LastFeedback);
    }


    [Test]
    public void ActivatePowerUp_PlayerMovementNotNull_DestroysGameObject()
    {
        dashPowerup.ActivatePowerUp();
        Assert.IsTrue(dashPowerup == null || dashPowerup.Equals(null));
    }
}