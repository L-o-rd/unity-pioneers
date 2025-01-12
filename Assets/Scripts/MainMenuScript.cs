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
        SoundManager.Instance.Load();
        stats.LoadStats();
    }

    public void Quit()
    {
        stats.SaveStats();
        SoundManager.Instance.Save();
        Application.Quit();
    }

    public void SwitchToScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }
}
