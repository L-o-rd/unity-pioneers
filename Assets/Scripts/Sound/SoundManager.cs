using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{

    public static SoundManager Instance { get; private set; }
    public float volume = .75f;

    [SerializeField]
    private Slider volumeSlider;

    private void Awake()
    {
        if (Instance is not null && Instance != this)
        {
            Destroy(this);
        } else
        {
            Instance = this;
            DontDestroyOnLoad(Instance);
        }
    }

    private void Start()
    {
        volumeSlider.onValueChanged.AddListener(delegate { volume = volumeSlider.value; });
    }
}
