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
        playerStats.LoadStats();
        UpdateButtons();
    }
    public void UpdateButtons()
    {
        healthButton.setUpgradeName("Max Health");
        healthButton.setUpgradeLevel(playerStats.status.maxHealthLevel);
        healthButton.setPrice(1 + playerStats.status.maxHealthLevel * 2);
        healthButton.buildButton();

        speedButton.setUpgradeName("Speed");
        speedButton.setUpgradeLevel(playerStats.status.movementSpeedLevel);
        speedButton.setPrice(1 + playerStats.status.movementSpeedLevel * 2);
        speedButton.buildButton();

        defenceButton.setUpgradeName("Defence");
        defenceButton.setUpgradeLevel(playerStats.status.defenceLevel);
        defenceButton.setPrice(1 + playerStats.status.defenceLevel * 2);
        defenceButton.buildButton();

        dmgButton.setUpgradeName("Damage");
        dmgButton.setUpgradeLevel(playerStats.status.bonusDamageLevel);
        dmgButton.setPrice(1 + playerStats.status.bonusDamageLevel * 2);
        dmgButton.buildButton();
    }
    public void SwitchToScene(string scene)
    {
        SceneManager.LoadScene(scene);
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
