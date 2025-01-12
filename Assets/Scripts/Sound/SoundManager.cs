using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    public static SoundManager Instance { get; private set; }
    public float volume = .75f;

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
        Load();
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

    public void PlaySound(AudioClip clip)
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
