using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishRun : MonoBehaviour
{
    [SerializeField]
    private TMP_Text text;

    private void Start()
    {
        text.text = $"Diamonds:  {RNGManager.Instance.diamonds}";
    }

    public void GoBack()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
