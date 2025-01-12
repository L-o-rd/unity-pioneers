using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishRun : MonoBehaviour
{
    [SerializeField]
    private TMP_Text text;

    [SerializeField]
    private AudioClip finishRunSound;
    private IEnumerator PlaySounds()
    {
        // If you want to add more sounds, modify this function
        SoundManager.Instance.PlaySound(finishRunSound);
        yield return null;
    }
    private void Start()
    {
        text.text = $"Diamonds:  {RNGManager.Instance.diamonds}";
        StartCoroutine(PlaySounds());
    }

    public void GoBack()
    {
        RNGManager.Instance.RandomSeed();
        SceneManager.LoadScene("MainMenu");
    }
}
