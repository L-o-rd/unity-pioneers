using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashPowerup : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerMovement playerMovement = collision.GetComponent<PlayerMovement>();
            if (playerMovement != null)
            {
                playerMovement.ActivateDashPower();
                Destroy(gameObject);
            }
        }
    }

}
