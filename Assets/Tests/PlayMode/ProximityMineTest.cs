using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class ProximityMineTests
{
    private GameObject player;

    private GameObject proximityMineObject;

    private ProximityMine proximityMine;

    [SetUp]
    public void SetUp()
    {
        // Create mock player
        player = new GameObject("Player");
        player.tag = "Player";
        player.AddComponent<MockPlayerStats>();
        player.AddComponent<BoxCollider2D>();

        proximityMineObject = new GameObject();
        proximityMine = proximityMineObject.AddComponent<ProximityMine>();
        proximityMine.testMode = true;
        proximityMineObject.AddComponent<BoxCollider2D>();

    }

    [TearDown]
    public void TearDown()
    {
        Object.DestroyImmediate(player);
        Object.DestroyImmediate(proximityMineObject);
    }

    [Test]
    public void ProximityMine_CheckProximityFindsPlayerInReach()
    {

        player.transform.position = Vector3.zero;
        proximityMine.transform.position = Vector3.zero;

        // Act
        proximityMine.CheckProximity();

        // Assert
        Assert.IsTrue(proximityMine.isTriggered, "Player should be in range of proximity mine.");
    }

        [Test]
    public void ProximityMine_CheckProximityDoesNotFindPlayerNotInReach()
    {

        player.transform.position = new Vector3(10,10,10);

        // Act
        proximityMine.CheckProximity();

        // Assert
        Assert.IsFalse(proximityMine.isTriggered, "Player should not be in range of proximity mine.");
    }

    //the Explode method in ProximityMine.cs is the same as the one in ExplosiveCrate.cs
    [Test]
    public void ProximityMine_TriggersExplosionInRange()
    {

        player.transform.position = Vector3.zero;

        proximityMine.Explode();

        // Assert
        //Assert.IsNull(proximityMine, "Proximity mine should be destroyed after explosion.");
        MockPlayerStats playerStats = player.GetComponent<MockPlayerStats>();
        Assert.AreEqual(proximityMine.getExplosionDamage(), playerStats.DamageTaken, "Player did not take correct explosion damage.");
    }
}