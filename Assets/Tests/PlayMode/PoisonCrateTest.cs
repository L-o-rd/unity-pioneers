using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class PoisonCrateTests
{
    private GameObject player;
    private GameObject enemy;
    private PoisonCrate poisonCrate;
    private GameObject poisonEffectPrefab;

    [SetUp]
    public void SetUp()
    {
        // Create mock player
        player = new GameObject("MockPlayer");
        player.AddComponent<MockPlayerStats>();
        player.AddComponent<BoxCollider2D>();
        player.tag = "Player";

        // Create mock enemy
        enemy = new GameObject("MockEnemy");
        enemy.AddComponent<MockEnemyHealth>();
        enemy.AddComponent<BoxCollider2D>();
        enemy.tag = "Enemy";

        // Load prefabs
        var crate = new GameObject();
        poisonCrate = crate.AddComponent<PoisonCrate>();
        poisonCrate.testMode = true;
        poisonEffectPrefab = new GameObject();
        poisonCrate.setTestPrefab(poisonEffectPrefab);

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
    public IEnumerator PoisonCrate_SpawnsPoisonEffectAndAppliesDamage()
    {

        player.transform.position = Vector3.zero;
        enemy.transform.position = Vector3.zero;

        // Act
        poisonCrate.InteractWithCrate();
        yield return new WaitForSeconds(6f); // Wait for poison duration + buffer

        // Assert
        MockPlayerStats playerStats = player.GetComponent<MockPlayerStats>();
        Assert.AreEqual(poisonCrate.getTotalPoisonDamage(), playerStats.DamageTaken, "Player did not take correct poison damage.");

        MockEnemyHealth enemyHealth = enemy.GetComponent<MockEnemyHealth>();
        Assert.AreEqual(poisonCrate.getTotalPoisonDamage(), enemyHealth.DamageTaken, "Enemy did not take correct poison damage.");
    }
}
