using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UI;

public class RageMeterTests
{
    private GameObject testObject;
    private RageMeter rageMeter;
    private Slider rageSlider;
    private MockPlayerStats mockPlayerStats;
    private RectTransform rageMeterTransform;

    private void SetPrivateField(object obj, string fieldName, object value)
    {
        var field = obj.GetType().GetField(fieldName, System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        field.SetValue(obj, value);
    }

    [SetUp]
    public void SetUp()
    {
        // Create a GameObject for testing
        testObject = new GameObject("RageMeterTestObject");
        rageMeter = testObject.AddComponent<RageMeter>();

        // Add and assign the rage slider
        var sliderObject = new GameObject("RageSlider");
        sliderObject.transform.SetParent(testObject.transform);
        rageSlider = sliderObject.AddComponent<Slider>();
        SetPrivateField(rageMeter,"rageSlider", rageSlider);

        // Mock the PlayerStats
        mockPlayerStats = testObject.AddComponent<MockPlayerStats>();

        // Add and assign RectTransform for shake effect
        rageMeterTransform = testObject.AddComponent<RectTransform>();
        SetPrivateField(rageMeter,"rageMeterTransform", rageMeterTransform);
        rageMeter.testMode = true;
        rageMeter.TestStart();


    }

    [TearDown]
    public void TearDown()
    {
        // Cleanup test object after each test
        Object.DestroyImmediate(testObject);
    }

    [Test]
    public void AddRage_IncreasesRageCorrectly()
    {
        rageMeter.AddRage(50f);
        float currentRage = rageMeter.getCurrentRage();
        rageMeter.TestUpdate();
        Assert.AreEqual(50f, currentRage);
        Assert.AreEqual(50f, rageSlider.value);
    }

    [Test]
    public void AddRage_DoesNotExceedMaxRage()
    {
        rageMeter.AddRage(250f);

        float currentRage = rageMeter.getCurrentRage();
        rageMeter.TestUpdate();
        Assert.AreEqual(200f, currentRage);
        Assert.AreEqual(200f, rageSlider.value);
    }

    [Test]
    public void AddRage_DoesNothingIfRageIsFull()
    {
        rageMeter.setCurrentRage(200f);

        rageMeter.AddRage(50f);
        rageMeter.TestUpdate();

        float currentRage = rageMeter.getCurrentRage();
        Assert.AreEqual(200f, currentRage); 
    }

    [UnityTest]
    public IEnumerator ActivateRage_StartsAndEndsCorrectly()
    {
        rageMeter.setCurrentRage(200f);

        yield return rageMeter.StartCoroutine(rageMeter.ActivateRage());
        yield return new WaitForSeconds(5f);

        float currentRage = rageMeter.getCurrentRage();
        bool isRageActive = rageMeter.isRageActive;

        Assert.AreEqual(0f, currentRage);
        Assert.IsFalse(isRageActive);
        Assert.AreEqual(mockPlayerStats.Damage, 0); // Damage boost removed
    }

    [UnityTest]
    public IEnumerator ActivateRage_AppliesDamageBoost()
    {
        mockPlayerStats.setPlayerDamage(20);
        rageMeter.setCurrentRage(200f);
        rageMeter.manualStop = true;

        yield return rageMeter.StartCoroutine(rageMeter.ActivateRage());
        yield return new WaitForSeconds(0.1f);
        
        int expectedDamage = 30; // Base damage (20) + boost (10)
        Assert.AreEqual(expectedDamage, mockPlayerStats.Damage);
    }

    [UnityTest]
    public IEnumerator ShakeMeter_ShakesAndResetsPosition()
    {
        // Arrange
        rageMeter.setCurrentRage(200f);
        Vector3 originalPosition = rageMeterTransform.localPosition;

        // Act
        yield return rageMeter.StartCoroutine(rageMeter.ShakeMeter());

        // Assert
        Assert.AreEqual(originalPosition, rageMeterTransform.localPosition);
    }
}
