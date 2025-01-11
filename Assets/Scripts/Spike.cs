using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour
{
    [SerializeField] private float activationInterval = 2f; // Seconds
    [SerializeField] private float damage = 10f;

    private bool isActive = false;
    private SpriteRenderer spriteRenderer;
    private float timer;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isActive && collision.CompareTag("Player"))
        {
            PlayerStats playerStats = collision.GetComponent<PlayerStats>();
            if (playerStats != null && !playerStats.isTrapImmune())
            {
                playerStats.TakeDamage(damage);
            }
        }
    }

    private void UpdateSprite()
    {
        spriteRenderer.color = isActive ? Color.black : Color.gray;
    }
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        damage = damage*GameObject.Find("RoomManager").GetComponent<RoomManager>().GetDifficulty();
        timer = activationInterval;
        UpdateSprite();
    }

    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            isActive = !isActive;
            timer = activationInterval;
            UpdateSprite();
        }
    }
}
