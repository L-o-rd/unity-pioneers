using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mud : MonoBehaviour
{
    private PlayerMovement playerMovement;
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player") && playerMovement != null && !playerMovement.IsImmuneToSlow()) 
        {
            playerMovement.setInMud(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && playerMovement != null)
        {
            playerMovement.setInMud(false);
        }
    }

    void Start()
    {
        playerMovement = FindObjectOfType<PlayerMovement>();
        if (playerMovement == null)
        {
            Debug.LogError("PlayerMovement not found in the scene");
        }
    }
}