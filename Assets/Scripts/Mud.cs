using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mud : MonoBehaviour
{
    private PlayerMovement playerMovement;
    public bool testMode = false;
    public void OnTriggerStay2D(Collider2D other)
    {
        if (testMode)
            FindObjectOfType<MockPlayerMovement>().speedDecreased = true;
        if (other.CompareTag("Player") && playerMovement != null && !playerMovement.IsImmuneToSlow()) 
        {
            playerMovement.setInMud(true);
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if (testMode)
            FindObjectOfType<MockPlayerMovement>().speedDecreased = false;
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
            Debug.LogWarning("PlayerMovement not found in the scene");
        }
    }
}