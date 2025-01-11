using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    [SerializeField]
    private PlayerStats stats;

    private void Start()
    {
        stats.LoadStats();
    }

    public void Quit()
    {
        stats.SaveStats();
        Application.Quit();
    }

    public void SwitchToScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }
}
