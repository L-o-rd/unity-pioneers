using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PowerupManager : MonoBehaviour
{
    [SerializeField] 
    protected string itemDescription;
    protected bool isPlayerNearby = false;
    protected PlayerMovement playerMovement;
    protected PlayerStats playerStats;
    public bool isPurchasable = false;
    private float price;

    void TryBuyPowerUp()
    {
        if (playerStats.getTotalCoins() >= price)
        {
            playerStats.addCoins(-price);
            ActivatePowerUp();
        }
        else
        {
            Debug.Log("Not enough coins. Current coins: " + playerStats.getTotalCoins() + ", Required coins: " + price);
        }
    }

    void Start()
    {
        if (isPurchasable)
        {
            price = Random.Range(50, 70)*GameObject.Find("RoomManager").GetComponent<RoomManager>().GetDifficulty();
        }
    }

    void Update()
    {
        if (isPurchasable && isPlayerNearby && Input.GetKeyDown(KeyCode.Space))
        {
            TryBuyPowerUp();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerMovement = other.GetComponent<PlayerMovement>();
            playerStats = other.GetComponent<PlayerStats>();
            isPlayerNearby = true;

            if (!isPurchasable) // Direct pickup if not purchasable
            {
                ActivatePowerUp();
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerMovement = null;
            isPlayerNearby = false;
        }
    }

    protected abstract void ActivatePowerUp();
}
