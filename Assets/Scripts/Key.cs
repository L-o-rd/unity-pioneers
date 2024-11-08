using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{

    bool hasTriggered = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (hasTriggered) return;
        Debug.Log("Checking collision with " + other.tag);
        if (other.CompareTag("Player"))
        {
            hasTriggered = true;
            Debug.Log("Key collected by " + other.name); // New Debug line
            Destroy(gameObject); // Despawn the key
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
