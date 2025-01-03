using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    public float GlobalDamageBoost { get; private set; }
    public void AddGlobalDamageBoost(float amount)
    {
        GlobalDamageBoost += amount;
        Debug.Log("Global Damage Boost modified by " + amount + ". Current boost: " + GlobalDamageBoost);
    }
}
