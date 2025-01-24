using System.Collections;
using NUnit.Framework;
using UnityEngine;

public class CoinTests
{
    private MockPlayerStats playerStatsMock;
    private GameObject playerObject;
    private Coin coin;
    private GameObject coinObject;

    private MockSoundManager soundManagerMock;

    [SetUp]
    public void Setup()
    {
        coinObject = new GameObject("MockCoin");
        coinObject.AddComponent<BoxCollider2D>();
        coinObject.GetComponent<BoxCollider2D>().isTrigger = true;
        coin = coinObject.AddComponent<Coin>();
        coin.testMode=true;
        coin.Start();

        // Mock PlayerStats
        playerObject = new GameObject("Player");
        playerStatsMock = playerObject.AddComponent<MockPlayerStats>();

        // Mock SoundManager
        soundManagerMock = new MockSoundManager();
        SoundManager.Instance = soundManagerMock;
    }

    [TearDown]
    public void TearDown()
    {
        Object.DestroyImmediate(coinObject);
        Object.DestroyImmediate(coin);
        Object.DestroyImmediate(playerObject);
    }

    [Test]
    public void Coin_AddsValueToPlayerStats_WhenPlayerPicksItUp(){
        // Arrange
        playerObject.tag = "Player";
        playerObject.AddComponent<BoxCollider2D>();

        // Act
        coin.OnTriggerEnter2D(playerObject.GetComponent<Collider2D>());

        // Assert
        Assert.IsTrue(playerStatsMock.CoinsAddedFlag);
        Assert.IsTrue(soundManagerMock.SoundPlayed);
    }

    [Test]
    public void Coin_DoesNotAddValueToPlayerStats_WhenNonPlayerPicksItUp(){
        // Arrange
        GameObject otherObject = new GameObject("Other");
        otherObject.tag = "Enemy";
        otherObject.AddComponent<BoxCollider2D>();

        // Act
        coin.OnTriggerEnter2D(otherObject.GetComponent<Collider2D>());

        // Assert
        Assert.IsFalse(playerStatsMock.CoinsAddedFlag);
        Assert.IsFalse(soundManagerMock.SoundPlayed);
    }

    [Test]
    public void Coin_DestroysItself_WhenPlayerPicksItUp()
    {
        // Arrange
        playerObject.tag = "Player";
        playerObject.AddComponent<BoxCollider2D>();

        // Act
        coin.OnTriggerEnter2D(playerObject.GetComponent<Collider2D>());
        // Assert
        Assert.IsTrue(coin == null || coin.Equals(null)); // Check if coin is destroyed
    }
}
