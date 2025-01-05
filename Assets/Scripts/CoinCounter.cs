using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoinCounter : MonoBehaviour
{
    [SerializeField]
    private PlayerStats playerStats;
    [SerializeField]
    private TMP_Text text;

    void Update()
    {
        text.text = playerStats.GetPermanentCoins().ToString();
    }
}
