using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InGameTextUI : MonoBehaviour
{

    [SerializeField]
    private TMP_Text descriptionText;

    [SerializeField]
    private TMP_Text worldText;

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

    public void ShowWorldFeedback(string text)
    {
        StartCoroutine(DisplayWorldFeedback(text, worldTextDuration));
    }

    private IEnumerator DisplayWorldFeedback(string text, float worldTextDuration)
    {
        worldText.text = text;
        worldText.gameObject.SetActive(true);
        yield return new WaitForSeconds(worldTextDuration);
        worldText.gameObject.SetActive(false);
    }


}
