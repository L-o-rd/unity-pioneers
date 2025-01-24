using NUnit.Framework;
using UnityEngine;

public class ChestTests
{
    private GameObject chestObject;
    private Chest chest;
    private GameObject playerObject;
    private RoomManagerMock roomManagerMock;
    private MockPlayerStats playerStatsMock;
    private MockSoundManager soundManagerMock;

    [SetUp]
    public void Setup()
    {

        // Mock RoomManager
        var roomManagerObject = new GameObject("RoomManager");
        roomManagerMock = roomManagerObject.AddComponent<RoomManagerMock>();
        roomManagerMock.SetDifficulty(1.5f);

        // Create the chest game object and attach the Chest script
        chestObject = new GameObject("MockChest");
        chestObject.AddComponent<BoxCollider2D>();
        chestObject.GetComponent<BoxCollider2D>().isTrigger = true;
        chest = chestObject.AddComponent<Chest>();

        // Initialize the chest manually
        chest.Initialize(roomManagerMock);

        // Mock PlayerStats
        playerObject = new GameObject("Player");
        playerStatsMock = playerObject.AddComponent<MockPlayerStats>();

        // Mock SoundManager
        soundManagerMock = new MockSoundManager();
        SoundManager.Instance = soundManagerMock;
        chest.testMode = true;
    }

    [TearDown]
    public void TearDown()
    {
        Object.DestroyImmediate(chestObject);
        Object.DestroyImmediate(playerObject);
        Object.DestroyImmediate(roomManagerMock.gameObject);
    }

    [Test]
    public void ChestDamagesPlayer_WhenTrapTriggered()
    {
        // Arrange
        //chestObject.tag = "MockChest";
        playerObject.tag = "Player";
        chestObject.AddComponent<BoxCollider2D>();
        playerObject.AddComponent<BoxCollider2D>();
        playerObject.GetComponent<Collider2D>().isTrigger = false;

        chest.RandomValueProvider = () => 0.2f; // Ensure deterministic randomness
        // Act
        chest.OnTriggerEnter2D(playerObject.GetComponent<Collider2D>());

        // Assert
        Assert.AreEqual(30, playerStatsMock.DamageTaken); // 20 * 1.5
        Assert.IsFalse(playerStatsMock.CoinsAddedFlag);
    }

    [Test]
    public void ChestRewardsPlayer_WhenNotATrap()
    {
        // Arrange
        //chestObject.tag = "Chest";
        playerObject.tag = "Player";
        playerObject.AddComponent<BoxCollider2D>();
        playerObject.GetComponent<Collider2D>().isTrigger = false;

        chest.RandomValueProvider = () => 0.9f; // Ensure deterministic randomness
        // Act
        chest.OnTriggerEnter2D(playerObject.GetComponent<Collider2D>());

        // Assert
        Assert.AreEqual(0, playerStatsMock.DamageTaken);
        Assert.AreEqual(90f, playerStatsMock.CoinsAdded); // 60 * 1.5
        Assert.IsTrue(soundManagerMock.SoundPlayed);
    }

    [Test]
    public void ChestDestroysSelf_AfterInteraction()
    {
        // Arrange
        //chestObject.tag = "Chest";
        playerObject.tag = "Player";
        playerObject.AddComponent<BoxCollider2D>();
        playerObject.GetComponent<Collider2D>().isTrigger = false;

        Random.InitState(1); // Ensure deterministic randomness

        // Act
        chest.OnTriggerEnter2D(playerObject.GetComponent<Collider2D>());

        // Assert
        Assert.IsTrue(chest == null); // Chest should be destroyed
    }

}
