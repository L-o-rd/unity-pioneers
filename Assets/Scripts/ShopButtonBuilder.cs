using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using TMPro;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.UI;

public class ShopButtonBuilder : MonoBehaviour
{
    [SerializeField]
    private string upgradeName;
    [SerializeField]
    private int price;
    [SerializeField]
    private int upgradeLevel;
    [SerializeField]
    private List<Sprite> sprites;
    void Start()
    {
        gameObject.transform.GetChild(1).GetComponent<TMP_Text>().text = upgradeName;
        gameObject.transform.GetChild(2).GetComponent<TMP_Text>().text = price.ToString();
        if(upgradeLevel < 0 || upgradeLevel > 6)
        {
            Debug.Log("Invalid upgrade level.");
        }
        else
        {
            gameObject.transform.GetChild(3).GetComponent<Image>().sprite = sprites[upgradeLevel];
        }
    }
}
