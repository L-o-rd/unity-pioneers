using NUnit.Framework;
using UnityEngine;
using TMPro;

public class PowerupManagerTests
{
    private GameObject powerupObject;
    private MockPowerup mockPowerup;
    private GameObject playerObject;
    private MockPlayerStats mockPlayerStats;

    [SetUp]
    public void SetUp()
    {
        // Set up the power-up object
        powerupObject = new GameObject();
        mockPowerup = powerupObject.AddComponent<MockPowerup>();
        mockPowerup.testMode=true;
        mockPowerup.isPurchasable = false;

        // Set up the player object
        playerObject = new GameObject("MockPlayerStats");
        mockPlayerStats = playerObject.AddComponent<MockPlayerStats>();
        
        playerObject.tag = "Player";

    }

    [TearDown]
    public void TearDown()
    {
        Object.DestroyImmediate(powerupObject);
        Object.DestroyImmediate(playerObject);
    }

    [Test]
    public void ActivatePowerUp_PlayerNearby_ActivatesPowerup()
    {
        mockPowerup.OnTriggerEnter2D(playerObject.AddComponent<BoxCollider2D>());

        mockPowerup.ActivatePowerUp();

        Assert.IsTrue(mockPowerup.powerupActivated);
    }

    [Test]
    public void TryBuyPowerUp_PlayerHasEnoughCoins_PurchasesSuccessfully()
    {
        // Arrange
        mockPowerup.isPurchasable = true;
        mockPowerup.Initialize();
        mockPowerup.OnTriggerEnter2D(playerObject.AddComponent<BoxCollider2D>());
        mockPlayerStats.addCoins(100);

        // Simulate pressing Space to buy
        //mockPowerup.Update();

        // Act
        mockPowerup.TryBuyPowerUp();

        // Assert
        Assert.IsTrue(mockPowerup.powerupActivated);
        Debug.Log(mockPlayerStats.CoinsAdded);
        Assert.AreEqual(100 - mockPowerup.getPrice(), mockPlayerStats.CoinsAdded);
    }

    [Test]
    public void TryBuyPowerUp_PlayerDoesNotHaveEnoughCoins_FailsToPurchase()
    {
        // Arrange
        mockPowerup.isPurchasable = true;
        mockPowerup.Initialize();
        mockPowerup.OnTriggerEnter2D(playerObject.AddComponent<BoxCollider2D>());
        mockPlayerStats.addCoins(10); // Player has only 10 coins

        // Simulate pressing Space to buy
        //mockPowerup.Update();

        // Act
        mockPowerup.TryBuyPowerUp();
        Debug.Log(mockPowerup.powerupActivated);
        // Assert
        Assert.IsFalse(mockPowerup.powerupActivated);
        Assert.AreEqual(10, mockPlayerStats.CoinsAdded);
    }

    [Test]
    public void CreatePriceText_PriceTextIsDisplayedWhenPlayerIsNearby()
    {
        // Arrange
        mockPowerup.isPurchasable = true;
        mockPowerup.Initialize();

        // Act
        mockPowerup.OnTriggerEnter2D(playerObject.AddComponent<BoxCollider2D>());

        // Assert
        var priceText = mockPowerup.GetComponent<MockPowerup>().priceText;
        Assert.IsNotNull(priceText);
        Assert.AreEqual($"Price: {mockPowerup.getPrice():F0}", priceText.text);
    }

    [Test]
    public void OnTriggerExit2D_HidesPriceText()
    {
        // Arrange
        mockPowerup.isPurchasable = true;
        mockPowerup.Initialize();
        mockPowerup.OnTriggerEnter2D(playerObject.AddComponent<BoxCollider2D>());

        // Act
        mockPowerup.OnTriggerExit2D(playerObject.GetComponent<BoxCollider2D>());

        // Assert
        var priceText = mockPowerup.GetComponent<MockPowerup>().priceText;
        Assert.IsFalse(priceText.gameObject.activeSelf);
    }
}
