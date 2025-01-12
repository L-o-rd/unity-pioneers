using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;

public class GameOverScreen : MonoBehaviour
{
    [SerializeField]
    private TMP_Text text;
    // Start is called before the first frame update

    [SerializeField]
    private AudioClip playerDeath;

    [SerializeField]
    private AudioClip gameOverSound;

    private IEnumerator PlaySounds(){
        Debug.Log("Playing sounds");
        SoundManager.Instance.PlaySound(playerDeath);
        yield return new WaitForSeconds(1);
        Debug.Log("Playing track");
        SoundManager.Instance.PlaySound(gameOverSound);
    }
    void Start()
    {
        text.text = $"Diamonds:  {RNGManager.Instance.diamonds}";
        StartCoroutine(PlaySounds());
    }

    public void Restart()
    {
        RNGManager.Instance.RandomSeed();
        SceneManager.LoadScene("RoomPrefab");
    }

    public void Exit()
    {
        RNGManager.Instance.RandomSeed();
        SceneManager.LoadScene("MainMenu");
    }
}
