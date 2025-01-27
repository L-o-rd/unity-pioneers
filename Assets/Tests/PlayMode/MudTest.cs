using NUnit.Framework;
using UnityEngine;

public class MudTests
{
    private GameObject player;
    private GameObject mudObject;
    private Mud mud;

    [SetUp]
    public void SetUp()
    {
        // Create mock player
        player = new GameObject("MockPlayer");
        player.tag = "Player";
        player.AddComponent<MockPlayerMovement>();
        player.AddComponent<BoxCollider2D>();

        mudObject = new GameObject();
        mud = mudObject.AddComponent<Mud>();
        mudObject.AddComponent<BoxCollider2D>();
        mud.testMode = true;
        
        
    }

    [TearDown]
    public void TearDown()
    {
        Object.Destroy(player);
    }

    [Test]
    public void Mud_SlowsPlayerWhenInside()
    {

        MockPlayerMovement playerMovement = player.GetComponent<MockPlayerMovement>();

        // Simulate player entering mud
        mud.OnTriggerStay2D(player.GetComponent<Collider2D>());

        // Assert
        Assert.IsTrue(playerMovement.speedDecreased, "Player should be slowed inside mud.");
    }

    [Test]
    public void Mud_StopsSlowingPlayerWhenOutside()
    {

        MockPlayerMovement playerMovement = player.GetComponent<MockPlayerMovement>();

        // Simulate player entering and exiting mud
        mud.OnTriggerStay2D(player.GetComponent<Collider2D>());
        mud.OnTriggerExit2D(player.GetComponent<Collider2D>());

        // Assert
        Assert.IsFalse(playerMovement.speedDecreased, "Player should not be slowed outside mud.");
    }
}
