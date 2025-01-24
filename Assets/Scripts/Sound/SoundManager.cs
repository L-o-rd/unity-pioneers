using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{

    public static SoundManager Instance { get; set; }
    public float volume = .75f;
    [SerializeField]
    public AudioSource audioSource;

    private void Awake()
    {
        if (Instance is not null && Instance != this)
        {
            Destroy(gameObject);
        } else
        {
            Instance = this;
            DontDestroyOnLoad(Instance);
        }
    }

    private void Start()
    {
        Load();
        audioSource.volume = volume;
    }

    public void Save()
    {
        FileStream file = File.Create(Application.persistentDataPath + "/options.dat");
        BinaryFormatter bf = new BinaryFormatter();
        bf.Serialize(file, this.volume);
        file.Close();
    }

    public void Load()
    {
        if (File.Exists(Application.persistentDataPath + "/options.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/options.dat", FileMode.Open);
            this.volume = (float) bf.Deserialize(file);
            file.Close();
        }
    }

    virtual public void PlaySound(AudioClip clip)
    {
        if (clip == null)
        {
            Debug.LogWarning("No audio clip provided to play.");
            return;
        }
        GameObject tempAudioSource = new GameObject("TempAudio");
        AudioSource audioSource = tempAudioSource.AddComponent<AudioSource>();
        audioSource.clip = clip;
        audioSource.volume = volume;
        audioSource.Play();
        Destroy(tempAudioSource, clip.length);
    }
}
