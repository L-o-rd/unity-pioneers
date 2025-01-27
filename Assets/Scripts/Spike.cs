using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour
{
    [SerializeField] private float activationInterval = 2f; // Seconds
    [SerializeField] private float damage = 10f;

    private bool isActive = false;

    public bool testMode = false;
    private SpriteRenderer spriteRenderer;
    private float timer;
    public float getDamage()
    {
        return damage;
    }

    public void setActive(bool active) //Test purposes
    {
        isActive = active;
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (isActive && collision.CompareTag("Player"))
        {
            if (testMode)
            {
                MockPlayerStats mockPlayerStats = collision.GetComponent<MockPlayerStats>();
                if (mockPlayerStats != null)
                {
                    mockPlayerStats.TakeDamage(damage);
                }
            }
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
