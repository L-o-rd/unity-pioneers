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
    void Start()
    {
        text.text = $"Diamonds:  {RNGManager.Instance.diamonds}";
    }

    public void Restart()
    {
        SceneManager.LoadScene("RoomPrefab");
    }

    public void Exit()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
