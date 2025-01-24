using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MockSoundManager : SoundManager
{
    public bool SoundPlayed { get; private set; }

    public override void PlaySound(AudioClip clip)
    {
        SoundPlayed = true;
    }
}
