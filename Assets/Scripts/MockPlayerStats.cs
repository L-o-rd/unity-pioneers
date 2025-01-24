using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MockPlayerStats : MonoBehaviour
{
    public float DamageTaken { get; private set; }
    public float CoinsAdded { get; private set; }
    public bool CoinsAddedFlag => CoinsAdded > 0;
    public bool TrapImmune { get; private set; }
    public int Damage { get; private set; }
    public float MaxHealth {get; set;}
    public void TakeDamage(float damage) => DamageTaken += damage;
    public void addCoins(float coins) => CoinsAdded += coins;
    public void setTrapImmune(bool value) => TrapImmune = value;
    public void setPlayerDamage(int damage) => Damage = damage;
    public void heal(float heal) => MaxHealth+=heal;
}
