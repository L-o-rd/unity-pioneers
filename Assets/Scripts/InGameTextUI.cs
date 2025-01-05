using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class InGameTextUI : MonoBehaviour
{

    [SerializeField]
    private TMP_Text descriptionText;

    [SerializeField]
    private TMP_Text worldText;

    [SerializeField]
    private TMP_Text coinsText;

    [SerializeField]
    private TMP_Text hpText;

    [SerializeField]
    private float duration = 2f;

    [SerializeField]

    private float worldTextDuration = 0.5f;

    public void ShowFeedback(string text)
    {
        StartCoroutine(DisplayFeedback(text, duration));
    }

    private IEnumerator DisplayFeedback(string text, float duration)
    {
        descriptionText.text = text;
        descriptionText.gameObject.SetActive(true);
        yield return new WaitForSeconds(duration);
        descriptionText.gameObject.SetActive(false);
    }

    public void ShowWorldFeedback(string text,Color color)
    {
        StartCoroutine(DisplayWorldFeedback(text, worldTextDuration,color));
    }

    private IEnumerator DisplayWorldFeedback(string text, float worldTextDuration, Color color)
    {
        worldText.text = text;
        worldText.color = color;
        worldText.gameObject.SetActive(true);
        yield return new WaitForSeconds(worldTextDuration);
        worldText.gameObject.SetActive(false);
    }

    public void UpdateCoinText(float coins)
    {
        coinsText.text = "Coins: " + coins;
    }

    public void UpdateHPText(float health){
        hpText.text = "Health: " + health;
    }
    void Update()
    {
        
    }


}
