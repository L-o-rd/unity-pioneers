using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopItem : MonoBehaviour
{
    public float price;
    private bool isPlayerNear = false;

    private PlayerStats playerStats;

    void Start()
    {
        price = Random.Range(50, 71)*GameObject.Find("RoomManager").GetComponent<RoomManager>().GetBonusDifficulty();
    }

    void Update()
    {
        if (isPlayerNear && Input.GetKeyDown(KeyCode.Space) && playerStats != null)
        {
            if (playerStats.getTotalCoins()>=price) // Assuming PlayerStats handles coins
            {
                playerStats.addCoins(-price);
                Debug.Log("Item bought!");
                Destroy(gameObject); // Remove the item after purchase
            }
            else
            {
                Debug.Log("Not enough coins!");
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerStats = other.GetComponent<PlayerStats>();
            isPlayerNear = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerStats=null;
            isPlayerNear = false;
        }
    }
}
