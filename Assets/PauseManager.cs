using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public static PauseManager Instance { get; private set; }
    private bool isPaused = false;
    private GameObject canvas;
    private void Awake()
    {
        if (Instance is not null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
            canvas = transform.GetChild(0).gameObject;
            DontDestroyOnLoad(Instance);
        }
    }

    void Update()
    {
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
            Time.timeScale = 1f;
            canvas.SetActive(false);
            isPaused = false;
        }
        else
        {
            Time.timeScale = 0f;
            canvas.SetActive(true);
            isPaused = true;
        }
    }
}
