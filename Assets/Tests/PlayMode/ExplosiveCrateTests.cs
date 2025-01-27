using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class ExplosiveCrateTests
{
    private GameObject player;
    private GameObject enemy;

    private GameObject explosiveCrateObject;
    private ExplosiveCrate explosiveCrate;
    private MockCrate mockCrate;

    [SetUp]
    public void SetUp()
    {
        // Create mock player
        player = new GameObject();
        player.AddComponent<MockPlayerStats>();
        player.AddComponent<BoxCollider2D>();
        player.tag = "Player";

        // Create mock enemy
        enemy = new GameObject();
        enemy.AddComponent<MockEnemyHealth>();
        enemy.AddComponent<BoxCollider2D>();
        enemy.tag = "Enemy";

        var crate = new GameObject();
        crate.tag = "Trap";
        mockCrate = crate.AddComponent<MockCrate>();
        crate.AddComponent<BoxCollider2D>();

        explosiveCrateObject = new GameObject();
        explosiveCrateObject.tag = "Trap";
        explosiveCrate = explosiveCrateObject.AddComponent<ExplosiveCrate>();
        explosiveCrateObject.AddComponent<BoxCollider2D>();
        explosiveCrate.testMode = true;

        var soundManager = new MockSoundManager();
        SoundManager.Instance = soundManager;

    }

    [TearDown]
    public void TearDown()
    {
        Object.Destroy(player);
        Object.Destroy(enemy);
    }

    [UnityTest]
    public IEnumerator ExplosiveCrate_TriggersExplosionAndDealsDamage()
    {
        player.transform.position = Vector3.zero;
        enemy.transform.position = Vector3.zero;
        explosiveCrate.transform.position = Vector3.zero;

        // Act
        explosiveCrate.Explode();
        yield return new WaitForSeconds(0.7f); // Wait for explosion delay

        // Assert
        MockPlayerStats playerStats = player.GetComponent<MockPlayerStats>();
        Assert.AreEqual(explosiveCrate.getExplosionDamage(), playerStats.DamageTaken, "Player did not take correct explosion damage.");

        MockEnemyHealth enemyHealth = enemy.GetComponent<MockEnemyHealth>();
        Assert.AreEqual(explosiveCrate.getExplosionDamage(), enemyHealth.DamageTaken, "Enemy did not take correct explosion damage.");

    }

    [UnityTest]
    public IEnumerator ExplosiveCrate_TriggersExplosionAndTriggersCratesInRange()
    {
        var crate = new GameObject();
        crate.tag = "Trap";
        MockCrate otherMockCrate = crate.AddComponent<MockCrate>();
        mockCrate.transform.position = explosiveCrate.transform.position;
        otherMockCrate.transform.position = explosiveCrate.transform.position + new Vector3(explosiveCrate.getExplosionRange()+1f, 0, 0);

        explosiveCrate.Explode();
        yield return new WaitForSeconds(0.7f); // Wait for explosion delay

        Assert.IsTrue(mockCrate.wasInteractedWith, "Explosive crate did not trigger other crate.");
        Assert.IsFalse(otherMockCrate.wasInteractedWith, "Explosive crate triggered other crate out of range.");

    }

}
