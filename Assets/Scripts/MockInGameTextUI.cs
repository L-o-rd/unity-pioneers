using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class MockInGameTextUI : MonoBehaviour
{
    public string LastFeedback { get; private set; }

    public string priceText { get; set; }

    public void ShowFeedback(string message)
    {
        LastFeedback = message;
    }
}
