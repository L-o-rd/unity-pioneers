using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour {
    private bool isDead = false;
    private float health;

    [SerializeField]
    private float maxHealth = 100;

    private void Start() {
        health = maxHealth;
    }

    private void Update() {
        if (isDead) return;

        if (Input.GetKey(KeyCode.Space)) {
            health--;
            Debug.Log(string.Format("Health remaining: {0}.", health));
        }

        if (health <= 0) {
            isDead = true;
        }
    }
}
