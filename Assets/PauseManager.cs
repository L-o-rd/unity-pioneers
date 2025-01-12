using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    public static PauseManager Instance { get; private set; }
    public bool isPaused = false;
    private GameObject canvas;
    private GameObject esys;
    private void Awake()
    {
        if (Instance is not null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            canvas = transform.GetChild(0).gameObject;
            esys = transform.GetChild(1).gameObject;
            DontDestroyOnLoad(Instance);
        }
    }

    void Update()
    {
        if (SceneManager.GetActiveScene().name != "RoomPrefab") return;
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    public void TogglePause()
    {
        Debug.Log("pause");
        if (isPaused)
        {
            esys.SetActive(false);
            canvas.SetActive(false);
            SoundManager.Instance.audioSource.Play();
            Time.timeScale = 1.0f;
            isPaused = false;
        }
        else
        {
            esys.SetActive(true);
            canvas.SetActive(true);
            SoundManager.Instance.audioSource.Pause();
            Time.timeScale = 0.0f;
            isPaused = true;
        }
    }

    public void Quit()
    {
        TogglePause();
        SceneManager.LoadScene("MainMenu");
    }
}
