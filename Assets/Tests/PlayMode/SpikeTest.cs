using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class SpikeTests
{
    private GameObject player;

    [SetUp]
    public void SetUp()
    {
        // Create mock player
        player = new GameObject();
        player.tag = "Player";
        player.AddComponent<MockPlayerStats>();
        player.AddComponent<BoxCollider2D>();


    }

    [TearDown]
    public void TearDown()
    {
        Object.Destroy(player);
    }

    [Test]
    public void Spike_DamagesPlayerWhenActive()
    {
        // Arrange
        GameObject spike = Object.Instantiate(new GameObject(), Vector3.zero, Quaternion.identity);
        player.transform.position = Vector3.zero;
        spike.AddComponent<BoxCollider2D>();
        Spike spikeTrap = spike.AddComponent<Spike>();
        spikeTrap.testMode = true;
        spikeTrap.setActive(true);

        // Simulate player entering trap
        spikeTrap.OnTriggerEnter2D(player.GetComponent<Collider2D>());

        // Assert
        MockPlayerStats playerStats = player.GetComponent<MockPlayerStats>();
        Assert.AreEqual(spikeTrap.getDamage(), playerStats.DamageTaken, "Player did not take correct spike damage.");
    }
}
