using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuScript : MonoBehaviour
{
    [SerializeField]
    private PlayerStats stats;

    [SerializeField]
    private Slider volumeSlider;

    private void Start()
    {
        SoundManager.Instance.Load();
        stats.LoadStats();

        volumeSlider.value = SoundManager.Instance.volume;
        volumeSlider.onValueChanged.AddListener(delegate { 
            SoundManager.Instance.volume = volumeSlider.value;
            SoundManager.Instance.audioSource.volume = volumeSlider.value;
        });
    }

    public void Quit()
    {
        stats.SaveStats();
        SoundManager.Instance.Save();
        Application.Quit();
    }

    public void SwitchToScene(string scene)
    {
        SoundManager.Instance.Save();
        SceneManager.LoadScene(scene);
    }
}
