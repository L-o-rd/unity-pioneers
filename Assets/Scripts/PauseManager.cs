using System.Collections;
using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    //private GameObject gameManager;
    private GameObject canvas;

    //this bool should be replaced when the gameManager is linked
    private bool gamePaused = false;

    void Start()
    {
        //TODO - get reference to gameManager;
        //gameManager = ;
        canvas = transform.GetChild(0).gameObject;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!gamePaused)
            {
                ShowPauseMenu();
            }
            else
            {
                HidePauseMenu();
            }
        }
    }

    public void ShowPauseMenu()
    {
        gamePaused = true;
        Time.timeScale = 0;
        canvas.SetActive(true);
    }

    public void HidePauseMenu()
    {
        gamePaused = false;
        Time.timeScale = 1;
        canvas.SetActive(false);
    }

    public void SaveAndQuit()
    {
        Debug.Log("Quitting run...\n");
        //TODO - implement ending run logic
    }
}
