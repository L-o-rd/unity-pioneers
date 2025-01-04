using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShopMenuScript : MonoBehaviour
{
    [SerializeField]
    private ShopButtonBuilder healthButton;
    [SerializeField]
    private ShopButtonBuilder speedButton;
    [SerializeField]
    private ShopButtonBuilder defenceButton;
    [SerializeField]
    private ShopButtonBuilder dmgButton;
    [SerializeField]
    private PlayerStats playerStats;

    void Start()
    {
        UpdateButtons();
    }
    public void UpdateButtons()
    {
        healthButton.setUpgradeName("Max Health");
        healthButton.setUpgradeLevel(playerStats.maxHealthLevel);
        healthButton.setPrice(1 + playerStats.maxHealthLevel * 2);
        healthButton.buildButton();

        speedButton.setUpgradeName("Speed");
        speedButton.setUpgradeLevel(playerStats.movementSpeedLevel);
        speedButton.setPrice(1 + playerStats.movementSpeedLevel * 2);
        speedButton.buildButton();

        defenceButton.setUpgradeName("Defence");
        defenceButton.setUpgradeLevel(playerStats.defenceLevel);
        defenceButton.setPrice(1 + playerStats.defenceLevel * 2);
        defenceButton.buildButton();

        dmgButton.setUpgradeName("Damage");
        dmgButton.setUpgradeLevel(playerStats.bonusDamageLevel);
        dmgButton.setPrice(1 + playerStats.bonusDamageLevel * 2);
        dmgButton.buildButton();
    }
    public void SwitchToScene(string test)
    {
        SceneManager.LoadScene(test);
    }
    public void UpgradeHealth()
    {
        playerStats.UpgradeMaxHealth();
        UpdateButtons();
    }

    public void UpgradeSpeed()
    {
        playerStats.UpgradeMovementSpeed();
        UpdateButtons();
    }

    public void UpgradeDmg()
    {
        playerStats.UpgradeBonusDamage();
        UpdateButtons();
    }

    public void UpgradeDef()
    {
        playerStats.UpgradeDefence();
        UpdateButtons();
    }
}
