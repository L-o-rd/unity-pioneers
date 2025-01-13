using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RNGManager : MonoBehaviour
{
    public static RNGManager Instance { get; private set; }
    public int Seed { get; set; } = 0;
    private System.Random srng;
    public System.Random rng;
    public int diamonds = 0;

    [SerializeField]
    private TMP_InputField input;

    private void Awake()
    {
        if (Instance is not null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(Instance);
        }
    }

    private void Start()
    {
        input.onEndEdit.AddListener(SetSeed);
        srng = new System.Random();
        this.Seed = srng.Next();
    }

    public void Make()
    {
        this.rng = new System.Random(this.Seed);
        Debug.Log($"Seed = {Seed}");
    }

    public void RandomSeed()
    {
        this.Seed = srng.Next();
    }

    public void SetSeed(string seed)
    {
        Int32 parsed = 0;
        if (Int32.TryParse(seed, out parsed))
        {
            Seed = parsed;
        } else
        {
            Seed = srng.Next();
        }

        Debug.Log($"Seed = {Seed}");
    }
}
