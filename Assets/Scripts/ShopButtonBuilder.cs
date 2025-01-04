using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using TMPro;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.UI;

public class ShopButtonBuilder : MonoBehaviour
{
    private string upgradeName;
    private int price;
    private int upgradeLevel;
    [SerializeField]
    private List<Sprite> sprites;
    [SerializeField]
    private PlayerStats playerStats;

    void Start()
    {
        buildButton();
    }

    public void setUpgradeName(string name)
    {
        this.upgradeName = name;
    }

    public void setPrice(int price)
    {
        this.price = price;
    }

    public void setUpgradeLevel(int level)
    {
        this.upgradeLevel = level;
    }

    public void buildButton()
    {
        gameObject.transform.GetChild(1).GetComponent<TMP_Text>().text = upgradeName;
        gameObject.transform.GetChild(2).GetComponent<TMP_Text>().text = price.ToString();
        if (upgradeLevel < 0 || upgradeLevel > 6)
        {
            Debug.Log("Invalid upgrade level.");
        }
        else
        {
            gameObject.transform.GetChild(3).GetComponent<Image>().sprite = sprites[upgradeLevel];
        }
    }
}
