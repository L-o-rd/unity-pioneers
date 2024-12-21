using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mud : MonoBehaviour
{
    [SerializeField] float slowFactor = 0.5f;
    private bool inMud = false; // to prevent multiple speed change calls and to fix dash bug
    private PlayerMovement playerMovement;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && playerMovement != null && !playerMovement.IsImmuneToSlow() && inMud == false) 
        {
            Debug.Log("Player detected");
            inMud = true;
            playerMovement.SlowPlayerBy(slowFactor);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && playerMovement != null && !playerMovement.IsImmuneToSlow() && inMud == true)
        {
            playerMovement.SpeedUpPlayerBy(slowFactor);
            inMud = false;
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