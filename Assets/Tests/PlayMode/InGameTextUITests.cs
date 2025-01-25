using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using TMPro;

public class InGameTextUITests
{
    private GameObject uiObject;
    private InGameTextUI inGameTextUI;
    private TMP_Text descriptionText;
    private TMP_Text worldText;
    private TMP_Text coinsText;
    private TMP_Text hpText;

    [SetUp]
    public void SetUp()
    {
        uiObject = new GameObject("InGameTextUI");
        inGameTextUI = uiObject.AddComponent<InGameTextUI>();

        descriptionText = CreateTMPText("DescriptionText");
        worldText = CreateTMPText("WorldText");
        coinsText = CreateTMPText("CoinsText");
        hpText = CreateTMPText("HPText");

        SetPrivateField(inGameTextUI, "descriptionText", descriptionText);
        SetPrivateField(inGameTextUI, "worldText", worldText);
        SetPrivateField(inGameTextUI, "coinsText", coinsText);
        SetPrivateField(inGameTextUI, "hpText", hpText);
    }

    [TearDown]
    public void TearDown()
    {
        Object.DestroyImmediate(uiObject);
    }

    private TMP_Text CreateTMPText(string name)
    {
        var textObject = new GameObject(name);
        textObject.transform.SetParent(uiObject.transform);
        return textObject.AddComponent<TextMeshProUGUI>();
    }

    private void SetPrivateField(object obj, string fieldName, object value)
    {
        var field = obj.GetType().GetField(fieldName, System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        field.SetValue(obj, value);
    }

    [UnityTest]
    public IEnumerator ShowFeedback_ActivatesAndDeactivatesText()
    {
        string testMessage = "Test Feedback";

        inGameTextUI.ShowFeedback(testMessage);

        Assert.AreEqual(testMessage, descriptionText.text);
        Assert.IsTrue(descriptionText.gameObject.activeSelf);

        yield return new WaitForSeconds(2f);

        Assert.IsFalse(descriptionText.gameObject.activeSelf);
    }

    [UnityTest]
    public IEnumerator ShowWorldFeedback_ActivatesAndDeactivatesText()
    {
        string testMessage = "World Feedback";
        Color testColor = Color.red;

        inGameTextUI.ShowWorldFeedback(testMessage, testColor);

        Assert.AreEqual(testMessage, worldText.text);
        Assert.AreEqual(testColor, worldText.color);
        Assert.IsTrue(worldText.gameObject.activeSelf);

        yield return new WaitForSeconds(0.5f);

        Assert.IsFalse(worldText.gameObject.activeSelf);
    }

    [Test]
    public void UpdateCoinText_SetsCorrectText()
    {
        float testCoins = 123;

        inGameTextUI.UpdateCoinText(testCoins);

        Assert.AreEqual("Coins: 123", coinsText.text);
    }

    [Test]
    public void UpdateHPText_SetsCorrectText()
    {
        float testHP = 75.5f;

        inGameTextUI.UpdateHPText(testHP);
        
        Assert.AreEqual("Health: 75.50", hpText.text);
    }
}