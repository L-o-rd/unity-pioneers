using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Crate : MonoBehaviour
{
    [SerializeField] protected AudioClip explosionSound;

    [SerializeField] protected AudioClip poisonSound;
    public abstract void InteractWithCrate();
}
